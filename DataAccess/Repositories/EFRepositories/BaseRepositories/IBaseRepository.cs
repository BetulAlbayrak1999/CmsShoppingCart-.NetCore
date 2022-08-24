using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.BaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> CreateAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(string Id);

        Task<T> GetByAsync(Expression<Func<T, bool>> predicate = null);

        Task<T> GetByIdAsync(string Id);

        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate = null);
    }
}
