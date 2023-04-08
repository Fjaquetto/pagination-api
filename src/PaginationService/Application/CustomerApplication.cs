using Bogus;
using Microsoft.Identity.Client;
using PaginationService.App.Adapters.DataContracts;
using PaginationService.App.Application.DataContracts;
using PaginationService.App.Application.ViewModels;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.Model;

namespace PaginationService.App.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerAdapter _customerAdapter;
        public CustomerApplication(
            ICustomerRepository customerRepository,
            ICustomerAdapter customerAdapter)
        {
            _customerRepository = customerRepository;
            _customerAdapter = customerAdapter;
        }

        public async Task<List<Customer>> SeedData()
        {
            var customers = new Faker<Customer>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .Generate(100);

            await _customerRepository.AddRangeAsync(customers);
            return customers;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersPaginated(int pageIndex, int pageSize)
        {
            var customers = await _customerRepository.GetAllCustomersPaginated(pageIndex, pageSize);
            return _customerAdapter.ConvertToCustomerViewModel(customers);
        }
    }
}
