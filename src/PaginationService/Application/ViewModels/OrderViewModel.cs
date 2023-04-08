using PaginationService.Domain.Model;

namespace PaginationService.App.Application.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Guid id, DateTime orderDate, decimal totalAmount, Guid customerId, IEnumerable<OrderDetailsViewModel> orderDetails)
        {
            Id = id;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            CustomerId = customerId;
            OrderDetails = orderDetails;
        }

        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Guid CustomerId { get; set; }

        public IEnumerable<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
