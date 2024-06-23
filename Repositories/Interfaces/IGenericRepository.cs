using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore_Backend.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task DeleteAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
    }
}
