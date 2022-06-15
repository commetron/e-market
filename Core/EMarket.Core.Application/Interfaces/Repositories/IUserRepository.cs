using System.Threading.Tasks;
using EMarket.Core.Application.ViewModels.User;
using EMarket.Core.Domain.Entities;

namespace EMarket.Core.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Login(LoginViewModel loginVm);
    }
}

