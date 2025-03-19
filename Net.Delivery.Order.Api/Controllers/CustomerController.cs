using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Api.Controllers
{
    /// <summary>
    /// Consumer Api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Manage a customer api controller
        /// </summary>
        /// <param name="userService">Customer service instance</param>
        public CustomerController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="items">Order items</param>
        /// <param name="customer">Order customer</param>
        [HttpPost("register-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            bool retorno = await _userService.Add(customer);
        }
    }
}
