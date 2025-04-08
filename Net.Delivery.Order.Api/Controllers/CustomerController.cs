using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Model.DTOs;
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
        /// Creates a customer
        /// Customer types: CLIENTE | EMPRESA
        /// </summary>
        [HttpPost("register-customer")]
        public async Task<ActionResult<bool>> CreateCustomer([FromBody] Customer customer)
        {
            bool retorno = await _userService.Add(customer);
            return new ActionResult<bool>(retorno);
        }

        /// <summary>
        /// return all customers
        /// Customer types: CLIENTE | EMPRESA
        /// </summary>
        [HttpGet()]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            List<CustomerDTO> retorno = await _userService.GetAll();
            return new ActionResult<List<CustomerDTO>>(retorno);
        }


    }
}
