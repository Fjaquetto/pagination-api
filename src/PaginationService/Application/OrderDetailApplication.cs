using Bogus;
using PaginationService.App.Application.DataContracts;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.Model;

namespace PaginationService.App.Application
{
    public class OrderDetailApplication : IOrderDetailApplication
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailApplication(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task SeedData(List<Order> orders)
        {
            var orderDetails = new Faker<OrderDetail>()
                .RuleFor(od => od.Id, f => Guid.NewGuid())
                .RuleFor(od => od.OrderId, f =>
                {
                    var order = orders[f.Random.Number(0, 100 - 1)];
                    return order.Id;
                })
                .RuleFor(od => od.Quantity, f => f.Random.Number(1, 10))
                .RuleFor(od => od.Price, f => f.Random.Decimal(0.01M, 100M))
                .Generate(100);

            await _orderDetailRepository.AddRangeAsync(orderDetails);
        }
    }
}
