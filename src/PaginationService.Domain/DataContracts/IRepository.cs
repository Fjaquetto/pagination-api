using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationService.Domain.DataContracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
