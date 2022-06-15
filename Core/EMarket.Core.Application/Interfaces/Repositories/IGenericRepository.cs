using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMarket.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> Add(Entity entity);
        Task Update(Entity entity);
        Task Delete(Entity entity);
        Task<List<Entity>> GetAll();
        Task<Entity> GetById(int id);
        Task<List<Entity>> GetAllWithInclude(List<string> properties);
    }
}
