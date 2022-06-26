using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Advertisements;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApp.EMarket.Middlewares;

namespace EMarket.WebApp.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public AdvertisementController(IAdvertisementService advertisementService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _advertisementService = advertisementService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _advertisementService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveAdvertisementViewModel vm = new();
            vm.Categories = await _categoryService.GetAllViewModel();

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdvertisementViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();

                return View("Save", vm);
            }

            SaveAdvertisementViewModel advertisementVm = await _advertisementService.Add(vm);

            if (advertisementVm.Id != 0 && advertisementVm != null)
            {
                advertisementVm.ImageUrl1 = UploadFile(vm.File1, advertisementVm.Id);
                advertisementVm.ImageUrl2 = "";
                advertisementVm.ImageUrl3 = "";
                advertisementVm.ImageUrl4 = "";

                if (vm.File2 != null)
                {
                    advertisementVm.ImageUrl2 = UploadFile(vm.File2, advertisementVm.Id);
                }

                if (vm.File3 != null)
                {
                    advertisementVm.ImageUrl3 = UploadFile(vm.File3, advertisementVm.Id);
                }

                if (vm.File4 != null)
                {
                    advertisementVm.ImageUrl4 = UploadFile(vm.File4, advertisementVm.Id);
                }

                await _advertisementService.Update(advertisementVm);
            }

            return RedirectToRoute(new { controller = "Advertisement", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveAdvertisementViewModel vm = await _advertisementService.GetByIdSaveViewModel(id);
            vm.Categories = await _categoryService.GetAllViewModel();

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdvertisementViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();

                return View("Save", vm);
            }

            SaveAdvertisementViewModel advertisementVm = await _advertisementService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl1 = UploadFile(vm.File1, vm.Id, true, advertisementVm.ImageUrl1);
            vm.ImageUrl2 = UploadFile(vm.File2, vm.Id, true, advertisementVm.ImageUrl2);
            vm.ImageUrl3 = UploadFile(vm.File3, vm.Id, true, advertisementVm.ImageUrl3);
            vm.ImageUrl4 = UploadFile(vm.File4, vm.Id, true, advertisementVm.ImageUrl4);

            await _advertisementService.Update(vm);

            return RedirectToRoute(new { controller = "Advertisement", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _advertisementService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _advertisementService.Delete(id);

            string basePath = $"/Images/Advertisements/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Advertisement", action = "Index" });
        }

        private static string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Advertisement/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (FileStream stream = new (fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
