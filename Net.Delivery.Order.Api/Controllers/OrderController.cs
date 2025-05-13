using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Model;
using Net.Delivery.Order.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Net.Delivery.Order.Api.Controllers
{
    /// <summary>
    /// Order Api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Creates an order api controller
        /// </summary>
        /// <param name="orderService">Order service instance</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="items">Order items -> must be the ids of the items available.</param>
        /// <param name="customerId">Order's customer's id -> must be a customer, not a company </param>
        [HttpPost("create-order/{customerId}")]
        public async Task<ActionResult<string>> CreateOrder([FromBody] IList<long> items, long customerId)
        {
            try
            {
                string retorno = await _orderService.CreateOrder(items, customerId);
                return new ActionResult<string>(retorno);
            }catch(Exception ex)
            {
                return new ActionResult<string>(ex.Message);
            }
        }

        /// <summary>
        /// Updates an order situation
        /// </summary>
        /// <param name="orderId">Order identification</param>
        /// <param name="orderSituation">Order situation</param>
        [HttpPut("update-order-situation")]
        public async void UpdateOrderSituation([FromForm] string orderId, [FromForm] OrderSituation orderSituation)
        {
            
            try
            {
                await _orderService.UpdateOrderSituation(orderId, orderSituation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets all orders to delivery
        /// </summary>
        [HttpGet("get-all-orders-to-delivery")]
        public IList<Domain.Entities.Order> GetAllOrdersToDelivery()
        {
           return _orderService.GetAllOrdersToDelivery();
        }
    }
}
