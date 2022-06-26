using System;

namespace EMarket.Core.Application.ViewModels.Advertisements
{
	public class AdvertisementViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
