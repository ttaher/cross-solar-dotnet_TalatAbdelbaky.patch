using System.Linq;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IGenericRepository<T>
    {
        bool Exist(int id);

        Task<T> GetAsync(int id);

        IQueryable<T> Query();

        Task<int> InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}