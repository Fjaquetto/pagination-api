using Microsoft.EntityFrameworkCore;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.Model;
using PaginationService.Infra.EF;

namespace PaginationService.Infra.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(PaginationContext context) : base(context) { }

        public async Task<List<Customer>> GetAllCustomersPaginated(int pageIndex, int pageSize)
        {
            var rowsToSkip = (pageIndex - 1) * pageSize;

            var customers = await DbSet
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderDetails)
                .OrderBy(c => c.Id)
                .Skip(rowsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return customers;
        }
    }
}

