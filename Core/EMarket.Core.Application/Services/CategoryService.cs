using Microsoft.AspNetCore.Http;
using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Categories;
using EMarket.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Update(SaveCategoryViewModel vm)
        {
            Category category = await _categoryRepository.GetById(vm.Id);
            category.Id = vm.Id;
            category.Name = vm.Name;
            category.Description = vm.Description;

            await _categoryRepository.Update(category);
        }

        public async Task<SaveCategoryViewModel> Add(SaveCategoryViewModel vm)
        {
            Category category = new();
            category.Name = vm.Name;
            category.Description = vm.Description;

            category = await _categoryRepository.Add(category);

            SaveCategoryViewModel categoryVm = new();
            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.Description = category.Description;

            return categoryVm;
        }

        public async Task Delete(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            await _categoryRepository.Delete(category);
        }

        public async Task<SaveCategoryViewModel> GetByIdSaveViewModel(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            SaveCategoryViewModel vm = new();
            vm.Id = category.Id;
            vm.Name = category.Name;
            vm.Description = category.Description;

            return vm;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            List<Category> categoryList = await _categoryRepository.GetAllWithInclude(new List<string> { "Advertisements" });

            return categoryList.Select(category => new CategoryViewModel
            {
                Name = category.Name,
                Description = category.Description,
                Id = category.Id,
                AdvertisementsQuantity = category.Advertisements.Count,
                UsersQuantity = category.Advertisements
                .GroupBy(advertisement => advertisement.UserId)
                .Select(y => y.First()).Count()
            }).ToList();
        }
    }
}
