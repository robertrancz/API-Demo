using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnifiApiDemo.Business;
using UnifiApiDemo.Business.Model;
using UnifiApiDemo.Models;

namespace UnifiApiDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        } 

        public IActionResult Index()
        {
            var folders = new UnifiFolders().GetFolders();
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Unifi Web API client application. MzML converter.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Robert Rancz";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> GetFolderNodes(string filter)
        {
            var foldersViewModel = new List<FolderViewModel>();

            try
            {
                List<Folder> folders = await new UnifiFolders().GetFolders();

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    folders = folders.Where(f => f.Path.Contains(filter)).ToList();
                }

                foldersViewModel = folders?.Where(f => f.ParentId == null)
                        .Select(folder => new FolderViewModel
                        {
                            Text = folder.Path,
                            Id = folder.Id,
                            ParentId = folder.ParentId,
                            Children = GetChildren(folders, folder.Id)
                        }).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                        
            
            return Json(foldersViewModel);
        }

        private List<FolderViewModel> GetChildren(List<Folder> folders, Guid? parentId)
        {
            return folders?.Where(l => l.ParentId == parentId)
                .Select(l => new FolderViewModel
                {
                    Text = l.Path.Substring(l.Path.LastIndexOf("/") + 1),
                    Id = l.Id,
                    ParentId = l.ParentId,
                    Children = GetChildren(folders, l.Id)
                }).ToList();
        }

        public PartialViewResult GetFolderItems(string folderId)
        {
            var folderItems = new List<Item>();
            Guid result = Guid.Empty;

            if (Guid.TryParse(folderId, out result))
            {
                folderItems = new UnifiFolders().GetItems(result).Result;
            }
            
            return PartialView("~/Views/Home/_FolderItems.cshtml", folderItems);
        }

        public async Task<IActionResult> SampleResult(Guid resultId)
        {
            var sampleResult = await new SampleResultsApi().GetSampleResult(resultId);

            var model = new SampleResultViewModel()
            {
                Id = sampleResult.Id,
                Description = sampleResult.Description,
                Name = sampleResult.Name,
                Sample = sampleResult.Sample,
                MzmlFileExistsOnServer = System.IO.File.Exists(Path.Combine(hostingEnvironment.ContentRootPath, $"Downloads\\{sampleResult.Name}.mzml"))
            };
            

            return View(model);
        }
        
        public PartialViewResult ConvertToMzML(Guid resultId)
        {
            var msConvertProcess = new Process();
            string pass = "";
            System.Security.SecureString securePassword = new System.Security.SecureString();
            foreach (char c in pass)
                securePassword.AppendChar(c);

            var model = new ModalDialogViewModel() { Title = "MzML Conversion Result" };
            try
            {
                msConvertProcess.StartInfo = new ProcessStartInfo()
                {
                    UserName = "",
                    Domain = "",
                    Password = securePassword,
                    //Verb = "runas",
                    WorkingDirectory = Path.Combine(hostingEnvironment.ContentRootPath, "Lib"),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    Arguments =
                        $"\"https://administrator:administrator42@unifiapi.waters.com:50034/unifi/v1/sampleresults({resultId})?identity=https://unifiapi.waters.com:50333&scope=unifi&secret=secret\" --zlib -v -o {Path.Combine(hostingEnvironment.ContentRootPath, "Downloads")}",
                    FileName = Path.Combine(hostingEnvironment.ContentRootPath, "Lib\\msconvert.exe"),
                };

                msConvertProcess.Start();

                // Wait for the process to exit, but not more than 1 hour (3600000ms).
                if (!msConvertProcess.WaitForExit(3600000))
                {
                    msConvertProcess.Kill();
                    string result = msConvertProcess.StandardOutput.ReadToEnd();
                    Response.WriteAsync(result + Environment.NewLine + "Process exit code: " + msConvertProcess.ExitCode);
                    msConvertProcess.Close();
                    model.MainContent = "Conversion aborted. The operation did not finish after 1 hour.\nPlease try again later.";
                    model.AdditionalContent = "This usually happens if the server is too busy or becomes unresponsive due to high load.";
                }

                if (msConvertProcess.ExitCode == 0)
                {
                    model.MainContent = "Conversion successful. You may download the mzml file.";
                    model.AdditionalContent = "";
                }
                else
                {
                    model.MainContent = "Conversion process terminated unexpectedly.";
                    model.AdditionalContent = $"Process exit code: {msConvertProcess.ExitCode}";
                }
            }
            catch (Exception ex)
            {
                model.MainContent = "An error occured.";
                model.AdditionalContent = ex.Message;
                throw;
            }
            finally
            {
                msConvertProcess?.Close();
            }            

            return PartialView("~/Views/Home/_ModalWindow.cshtml", model);
        }

        public async Task<IActionResult> DownloadMzmlFile(string fileName)
        {
            string filePath = Path.Combine(hostingEnvironment.ContentRootPath, $"Downloads\\{fileName}.mzml");
            if (System.IO.File.Exists(filePath))
            {
                var memoryStream = new MemoryStream();
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;
                return File(memoryStream, "application/octet-stream", $"{fileName}.mzml");
            }
            return Json($"The {fileName} file was not found. Please convert first.");
        }
    }
}
