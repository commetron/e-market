using EMarket.Core.Application.ViewModels.Advertisements;
using System.Collections.Generic;

namespace EMarket.Core.Application.ViewModels.User
{
	public class UserViewModel
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<AdvertisementViewModel> Advertisements { get; set; }
    }
}

