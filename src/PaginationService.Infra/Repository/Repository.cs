using Microsoft.EntityFrameworkCore;
using PaginationService.Domain.DataContracts;
using PaginationService.Infra.EF;

namespace PaginationService.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PaginationContext _dbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(PaginationContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await DbSet.AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
