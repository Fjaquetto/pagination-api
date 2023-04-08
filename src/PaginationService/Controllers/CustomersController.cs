using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaginationService.App.Application.DataContracts;
using PaginationService.App.Application.ViewModels;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.Model;
using System.Diagnostics;

namespace PaginationService.App.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;
        private readonly IOrderApplication _orderApplication;
        private readonly IOrderDetailApplication _orderDetailApplication;

        public CustomersController(
            ICustomerApplication customerApplication,
            IOrderApplication orderApplication,
            IOrderDetailApplication orderDetailApplication)
        {
            _customerApplication = customerApplication;
            _orderApplication = orderApplication;
            _orderDetailApplication = orderDetailApplication;
        }

        [HttpPost]
        [Route("create-data")]
        public async Task<IActionResult> CreateData()
        {
            var customers = await _customerApplication.SeedData();
            var orders = await _orderApplication.SeedData(customers);
            await _orderDetailApplication.SeedData(orders);

            return Ok();
        }

        [HttpGet]
        [Route("customers")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAllDataPaginated([FromQuery] PagedRequestViewModel page)
        {
            var sw = new Stopwatch();
            sw.Start();
            var customers = await _customerApplication.GetAllCustomersPaginated(page.PageIndex, page.PageSize);
            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
            return Ok(customers);
        }
    }
}
