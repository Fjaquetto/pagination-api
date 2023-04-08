using Microsoft.EntityFrameworkCore;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.DataContracts.Cache;
using PaginationService.Domain.Model;
using PaginationService.Infra.EF;
using PaginationService.Infra.Repository;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly ICacheService<List<Customer>> _cacheService;

    public CustomerRepository(PaginationContext context,
        ICacheService<List<Customer>> cacheService) : base(context)
    {
        _cacheService = cacheService;
    }

    public async Task<List<Customer>> GetAllCustomersPaginated(int pageIndex, int pageSize)
    {
        var cacheKey = $"Customers_Page{pageIndex}_Size{pageSize}";

        var cachedCustomers = await _cacheService.GetAsync(cacheKey);
        if (cachedCustomers != null)
        {
            return cachedCustomers;
        }

        var rowsToSkip = (pageIndex - 1) * pageSize;

        var customers = await DbSet
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
            .OrderBy(c => c.Id)
            .Skip(rowsToSkip)
            .Take(pageSize)
            .ToListAsync();

        await _cacheService.SetAsync(cacheKey, customers);

        return customers;
    }
}