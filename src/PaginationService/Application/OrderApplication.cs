using Bogus;
using PaginationService.App.Application.DataContracts;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.Model;
using PaginationService.Infra.Repository;

namespace PaginationService.App.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        public OrderApplication(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> SeedData(List<Customer> customers)
        {
            var orders = new Faker<Order>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.CustomerId, f =>
                {
                    var customer = customers[f.Random.Number(0, 100 - 1)];
                    return customer.Id;
                })
                .RuleFor(o => o.OrderDate, f => f.Date.Past())
                .Generate(100);

            await _orderRepository.AddRangeAsync(orders);
            return orders;
        }
    }
}
