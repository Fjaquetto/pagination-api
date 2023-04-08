using PaginationService.App.Application.ViewModels;
using PaginationService.Domain.Model;

namespace PaginationService.App.Adapters.DataContracts
{
    public interface ICustomerAdapter
    {
        IEnumerable<CustomerViewModel> ConvertToCustomerViewModel(IEnumerable<Customer> entity);
    }
}
