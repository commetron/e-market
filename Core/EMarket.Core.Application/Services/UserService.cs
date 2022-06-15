using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.User;
using EMarket.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            UserViewModel userVm = new();
            User user = await _userRepository.Login(vm);

            if (user != null)
            {
                userVm.Id = user.Id;
                userVm.Name = user.Name;
                userVm.Username = user.Username;
                userVm.Phone = user.Phone;
                userVm.Password = user.Password;
                userVm.Email = user.Email;

                return userVm;
            }

            return null;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = await _userRepository.GetById(vm.Id);
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;

            await _userRepository.Update(user);
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;

            user = await _userRepository.Add(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Phone = user.Phone;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;

            return userVm;
        }

        public async Task Delete(int id)
        {
            User user = await _userRepository.GetById(id);
            await _userRepository.Delete(user);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            User user = await _userRepository.GetById(id);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.Username = user.Username;
            vm.Password = user.Password;
            vm.Phone = user.Phone;
            vm.Email = user.Email;

            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            List<User> userList = await _userRepository.GetAllWithInclude(new List<string> { "Advertisement" });

            return userList.Select(user => new UserViewModel
            {
                Name = user.Name,
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone
            }).ToList();
        }

    }
}
