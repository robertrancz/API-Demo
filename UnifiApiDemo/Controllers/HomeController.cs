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

        public ActionResult CallUnifiApi()
        {
            UnifiAPIViewModel model = new UnifiAPIViewModel();
            model.AnalyticalDataTypes = new List<SelectListItem>()
            { new SelectListItem() { Text = "MSe", Value = "MSe", Selected = true },
              new SelectListItem() { Text = "HDMSe", Value = "HDMSe" },
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CallUnifiApi(UnifiAPIViewModel model)
        {
            try
            {
                SampleResultToMzMLConverter.ConvertToMzml(model);
            }
            catch(Exception ex)
            {

            }
            return View(model);
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
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
                Sample = sampleResult.Sample
            };
            

            return View(model);
        }
        
        public void ConvertToMzML(Guid resultId)
        {
            var msConvertProcess = new Process();
            string pass = "spring2018";
            System.Security.SecureString securePassword = new System.Security.SecureString();
            foreach (char c in pass)
                securePassword.AppendChar(c);
            try
            {
                msConvertProcess.StartInfo = new ProcessStartInfo()
                {
                    UserName = "rorran",
                    Domain = "CORP",
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
                }
            }
            catch (Exception ex)
            {
                Response.WriteAsync(ex.Message);
                throw;
            }
            finally
            {
                msConvertProcess?.Close();
            }
        }
    }
}
