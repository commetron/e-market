using System.Collections.Generic;

namespace EMarket.Core.Application.ViewModels.Advertisements
{
	public class FilterAdvertisementViewModel
	{ 
		public string Name { get; set; }
		public List<int> CategoriesIds { get; set; }
	}
}
