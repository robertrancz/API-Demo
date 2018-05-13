using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnifiApiDemo.Business;
using UnifiApiDemo.Business.Model;
using UnifiApiDemo.Models;

namespace UnifiApiDemo.Controllers
{
    public class HomeController : Controller
    {
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
    }
}
