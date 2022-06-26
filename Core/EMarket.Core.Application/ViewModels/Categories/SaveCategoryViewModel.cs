using System.ComponentModel.DataAnnotations;

namespace EMarket.Core.Application.ViewModels.Categories
{
	public class SaveCategoryViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter a description.")]
		public string Description { get; set; }
	}
}

