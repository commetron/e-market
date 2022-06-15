using System.ComponentModel.DataAnnotations;

namespace EMarket.Core.Application.ViewModels.Categories
{
	public class SaveCategoryViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a category name.")]
		public string Name { get; set; }

		public string Description { get; set; }
	}
}

