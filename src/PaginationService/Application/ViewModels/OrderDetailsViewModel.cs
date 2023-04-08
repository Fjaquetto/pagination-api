namespace PaginationService.App.Application.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(Guid id, int quantity, decimal price, Guid orderId)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
            OrderId = orderId;
        }

        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid OrderId { get; set; }
    }
}
