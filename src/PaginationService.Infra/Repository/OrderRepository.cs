using PaginationService.Domain.DataContracts;
using PaginationService.Domain.Model;
using PaginationService.Infra.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationService.Infra.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(PaginationContext context) : base(context) { }
    }
}
