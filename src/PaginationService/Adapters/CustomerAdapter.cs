using PaginationService.App.Adapters.DataContracts;
using PaginationService.App.Application.ViewModels;
using PaginationService.Domain.Model;

namespace PaginationService.App.Adapters
{
    public class CustomerAdapter : ICustomerAdapter
    {
        public IEnumerable<CustomerViewModel> ConvertToCustomerViewModel(IEnumerable<Customer> entity)
        {
            foreach (var item in entity)
            {
                yield return new CustomerViewModel(item.Id, item.Name, item.Email, ConvertToOrderViewModel(item));
            }
        }

        private IEnumerable<OrderViewModel> ConvertToOrderViewModel(Customer entity)
        {
            foreach (var item in entity.Orders)
            {
                yield return new OrderViewModel(item.Id, item.OrderDate, item.TotalAmount, item.CustomerId, ConvertToOrderDetailsViewModel(item));
            }
        }

        private IEnumerable<OrderDetailsViewModel> ConvertToOrderDetailsViewModel(Order entity)
        {
            foreach (var item in entity.OrderDetails)
            {
                yield return new OrderDetailsViewModel(item.Id, item.Quantity, item.Price, item.OrderId);
            }
        }
    }
}
