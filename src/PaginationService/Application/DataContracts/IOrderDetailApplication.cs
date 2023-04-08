using PaginationService.Domain.Model;

namespace PaginationService.App.Application.DataContracts
{
    public interface IOrderDetailApplication
    {
        Task SeedData(List<Order> orders);
    }
}
