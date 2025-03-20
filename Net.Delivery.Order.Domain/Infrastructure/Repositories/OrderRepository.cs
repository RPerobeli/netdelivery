using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Infrastructure.Repositories
{
    /// <summary>
    /// Order repository
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly NetDeliveryContext _context;

        public OrderRepository(NetDeliveryContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Add an order into database
        /// </summary>
        /// <param name="order">Order's data</param>
        public string Add(Entities.Order order, List<Item> itemList)
        {
            foreach(var item in itemList)
            {
                order.OrderItens.Add(new OrderItem() { Item = item });
            }
            _context.Pedidos.Add(order);
            Commit();
            return order.OrderId;
        }

        /// <summary>
        /// Get all orders from database
        /// </summary>
        /// <returns>All orders</returns>
        public IList<Entities.Order> GetAll()
        {
            return _context.Pedidos.ToList();
        }
        /// <summary>
        /// Get an order from database by its id
        /// </summary>
        /// <returns>Order related to the id asked</returns>
        public Entities.Order GetOrderById(string OrderId)
        {
            Entities.Order orderRecovered = _context.Pedidos.FirstOrDefault(o => o.OrderId.Equals(OrderId));

            if (orderRecovered == null)
                throw new Exception("Order not found");

            return orderRecovered;
        }

        /// <summary>
        /// Get all orders from database for determined situation
        /// </summary>
        /// <returns>All orders those have the requested situation</returns>
        public IList<Entities.Order> GetOrdersBySituation(OrderSituation orderSituation)
        {
            return _context.Pedidos.ToList().Where(o=>o.OrderSituation == orderSituation).ToList();
        }

        /// <summary>
        /// Update an order to database
        /// </summary>
        /// <param name="order">Order's data</param>
        public void Update(Entities.Order order)
        {
            _context.Update(order);
        }
    }
}
