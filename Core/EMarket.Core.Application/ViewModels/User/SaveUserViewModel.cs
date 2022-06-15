using System.ComponentModel.DataAnnotations;

namespace EMarket.Core.Application.ViewModels.User
{
	public class SaveUserViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an username.")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords are not the same.")]
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
    }
}

