using PaginationService.Domain.Model;

namespace PaginationService.App.Application.DataContracts
{
    public interface IOrderApplication
    {
        Task<List<Order>> SeedData(List<Customer> customers);
    }
}
