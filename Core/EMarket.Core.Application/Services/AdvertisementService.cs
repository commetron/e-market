using Microsoft.AspNetCore.Http;
using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Advertisements;
using EMarket.Core.Application.ViewModels.User;
using EMarket.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMarket.Core.Application.Helpers;
using System;

namespace EMarket.Core.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public AdvertisementService(IAdvertisementRepository advertisementRepository, IHttpContextAccessor httpContextAccessor)
        {
            _advertisementRepository = advertisementRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task Update(SaveAdvertisementViewModel vm)
        {
            Advertisement advertisement = await _advertisementRepository.GetById(vm.Id);
            advertisement.Id = vm.Id;
            advertisement.Name = vm.Name;
            advertisement.Price = vm.Price;
            advertisement.Description = vm.Description;
            advertisement.ImageUrl1 = vm.ImageUrl1;
            advertisement.ImageUrl2 = vm.ImageUrl2;
            advertisement.ImageUrl3 = vm.ImageUrl3;
            advertisement.ImageUrl4 = vm.ImageUrl4;
            advertisement.CategoryId = vm.CategoryId;

            await _advertisementRepository.Update(advertisement);
        }

        public async Task<SaveAdvertisementViewModel> Add(SaveAdvertisementViewModel vm)
        {
            Advertisement advertisement = new();
            advertisement.Name = vm.Name;
            advertisement.Price = vm.Price;
            advertisement.ImageUrl1 = vm.ImageUrl1;
            advertisement.ImageUrl2 = vm.ImageUrl2;
            advertisement.ImageUrl3 = vm.ImageUrl3;
            advertisement.ImageUrl4 = vm.ImageUrl4;
            advertisement.Description = vm.Description;
            advertisement.CategoryId = vm.CategoryId;
            advertisement.Date = DateTime.Now;
            advertisement.UserId = _userViewModel.Id;
            advertisement.UserName = _userViewModel.Name;
            advertisement.UserMail = _userViewModel.Email;
            advertisement.UserPhone = _userViewModel.Phone;

            advertisement = await _advertisementRepository.Add(advertisement);

            SaveAdvertisementViewModel advertisementVm = new();

            advertisementVm.Id = advertisement.Id;
            advertisementVm.Name = advertisement.Name;
            advertisementVm.Price = advertisement.Price;
            advertisementVm.ImageUrl1 = advertisement.ImageUrl1;
            advertisementVm.ImageUrl2 = advertisement.ImageUrl2;
            advertisementVm.ImageUrl3 = advertisement.ImageUrl3;
            advertisementVm.ImageUrl4 = advertisement.ImageUrl4;
            advertisementVm.Description = advertisement.Description;
            advertisementVm.CategoryId = advertisement.CategoryId;

            return advertisementVm;
        }

        public async Task Delete(int id)
        {
            Advertisement advertisement = await _advertisementRepository.GetById(id);
            await _advertisementRepository.Delete(advertisement);
        }

        public async Task<List<AdvertisementViewModel>> GetAllViewModel()
        {
            List<Advertisement> advertisementList = await _advertisementRepository.GetAllWithInclude(new List<string> { "Category" });

            return advertisementList.Where(advertisement => advertisement.UserId == _userViewModel.Id).Select(advertisement => new AdvertisementViewModel
            {
                Name = advertisement.Name,
                Description = advertisement.Description,
                Id = advertisement.Id,
                Price = advertisement.Price,
                ImageUrl1 = advertisement.ImageUrl1,
                CategoryName = advertisement.Category.Name,
                CategoryId = advertisement.Category.Id,
            }).ToList();
        }

        public async Task<SaveAdvertisementViewModel> GetByIdSaveViewModel(int id)
        {
            Advertisement advertisement = await _advertisementRepository.GetById(id);

            SaveAdvertisementViewModel vm = new();
            vm.Id = advertisement.Id;
            vm.Name = advertisement.Name;
            vm.Description = advertisement.Description;
            vm.CategoryId = advertisement.CategoryId;
            vm.Price = advertisement.Price;
            vm.ImageUrl1 = advertisement.ImageUrl1;
            vm.ImageUrl2 = advertisement.ImageUrl2;
            vm.ImageUrl3 = advertisement.ImageUrl3;
            vm.ImageUrl4 = advertisement.ImageUrl4;

            return vm;
        }

        public async Task<List<AdvertisementViewModel>> GetAllViewModelHome()
        {
            List<Advertisement> advertisementList = await _advertisementRepository.GetAllWithInclude(new List<string> { "Category" });

            return advertisementList.Where(advertisement => advertisement.UserId != _userViewModel.Id).Select(advertisement => new AdvertisementViewModel
            {
                Name = advertisement.Name,
                Description = advertisement.Description,
                Id = advertisement.Id,
                Price = advertisement.Price,
                ImageUrl1 = advertisement.ImageUrl1,
                CategoryName = advertisement.Category.Name,
                CategoryId = advertisement.Category.Id,
            }).ToList();
        }

        public async Task<List<AdvertisementViewModel>> GetAllViewModelWithFilters(FilterAdvertisementViewModel filters)
        {
            List<Advertisement> advertisementList = await _advertisementRepository.GetAllWithInclude(new List<string> { "Category" });

            List<AdvertisementViewModel> listViewModels = advertisementList.Where(advertisement => advertisement.UserId != _userViewModel.Id).Select(advertisement => new AdvertisementViewModel
            {
                Name = advertisement.Name,
                Description = advertisement.Description,
                Id = advertisement.Id,
                Price = advertisement.Price,
                ImageUrl1 = advertisement.ImageUrl1,
                CategoryName = advertisement.Category.Name,
                CategoryId = advertisement.Category.Id,
            }).ToList();

            if (filters.Name != null)
            {
                listViewModels = listViewModels
                    .Where(advertisement => advertisement.Name.ToLower().Contains(filters.Name.ToLower()))
                    .ToList();
            }

            if (filters.CategoriesIds.Count > 0)
            {
                listViewModels = listViewModels
                    .Where(advertisement => filters.CategoriesIds.Contains(advertisement.CategoryId))
                    .ToList();
            }

            return listViewModels;
        }

        public async Task<AdvertisementViewModel> GetByIdViewModel(int id)
        {
            Advertisement advertisement = await _advertisementRepository.GetById(id);

            AdvertisementViewModel vm = new();
            vm.Id = advertisement.Id;
            vm.Name = advertisement.Name;
            vm.Description = advertisement.Description;
            vm.CategoryId = advertisement.Category.Id;
            vm.CategoryName = advertisement.Category.Name;
            vm.Price = advertisement.Price;
            vm.ImageUrl1 = advertisement.ImageUrl1;
            vm.ImageUrl2 = advertisement.ImageUrl2;
            vm.ImageUrl3 = advertisement.ImageUrl3;
            vm.ImageUrl4 = advertisement.ImageUrl4;
            vm.Date = advertisement.Date;
            vm.UserName = advertisement.UserName;
            vm.UserMail = advertisement.UserMail;
            vm.UserPhone = advertisement.UserPhone;

            return vm;
        }
    }
}
