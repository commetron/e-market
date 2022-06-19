using Microsoft.EntityFrameworkCore;
using EMarket.Core.Application.Helpers;
using EMarket.Core.Application.Interfaces.Repositories;
using EMarket.Core.Application.ViewModels.User;
using EMarket.Core.Domain.Entities;
using EMarket.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace EMarket.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> Add(User entity)
        {
            entity.Password = PasswordEncryptHelper.ComputeSha256Hash(entity.Password);
            await base.Add(entity);

            return entity;
        }

        public async Task<User> Login(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptHelper.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == passwordEncrypt);
            return user;
        }

    }
}
