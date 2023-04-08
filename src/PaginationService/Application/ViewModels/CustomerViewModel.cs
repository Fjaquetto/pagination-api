namespace PaginationService.App.Application.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel(Guid id, string name, string email, IEnumerable<OrderViewModel> orders)
        {
            Id = id;
            Name = name;
            Email = email;
            Orders = orders;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
