using Microsoft.AspNetCore.Http;
using EMarket.Core.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMarket.Core.Application.ViewModels.Advertisements
{
	public class SaveAdvertisementViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        public string ImageUrl1 { get; set; }

        [DataType(DataType.Text)]
        public string ImageUrl2 { get; set; }

        [DataType(DataType.Text)]
        public string ImageUrl3 { get; set; }

        [DataType(DataType.Text)]
        public string ImageUrl4 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a category.")]
        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File4 { get; set; }
    }
}

