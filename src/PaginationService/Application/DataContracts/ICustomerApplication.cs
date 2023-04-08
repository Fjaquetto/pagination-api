using PaginationService.App.Application.ViewModels;
using PaginationService.Domain.Model;

namespace PaginationService.App.Application.DataContracts
{
    public interface ICustomerApplication
    {
        Task<List<Customer>> SeedData();
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersPaginated(int pageIndex, int pageSize);
    }
}
