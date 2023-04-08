using PaginationService.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationService.Domain.DataContracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Customer>> GetAllCustomersPaginated(int pageIndex, int pageSize);
    }
}
