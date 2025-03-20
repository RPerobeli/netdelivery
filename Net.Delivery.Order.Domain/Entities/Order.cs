using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Net.Delivery.Order.Domain.Model;

namespace Net.Delivery.Order.Domain.Entities
{
    /// <summary>
    /// Order entity
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderId { get; set; }

        /// <summary>
        /// Order date
        /// </summary>
        public DateTime OrderCreateDate { get; set; }

        /// <summary>
        /// Order last update
        /// </summary>
        public DateTime OrderLastUpdate { get; set; }

        /// <summary>
        /// Order`s situation
        /// </summary>
        public OrderSituation OrderSituation { get; set; }

        /// <summary>
        /// Order`s items
        /// </summary>
        public ICollection<OrderItem> OrderItens { get; set; } = new List<OrderItem>();


        /// <summary>
        /// Order`s customer
        /// </summary>

        public long CustomerId { get; set; }

        //Relacionamentos
        public Customer Customer { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();



        /// <summary>
        /// Order builder
        /// </summary>
        /// <param name="items">Order items -> list of items id</param>
        /// <param name="customer">Order customer</param>
        public Order(IList<long> items, Customer customer)
        {
            OrderId = Guid.NewGuid().ToString();
            OrderCreateDate = DateTime.Now;
            OrderLastUpdate = OrderCreateDate;
            OrderSituation = OrderSituation.CREATED;
            Customer = customer;
        }

        public Order()
        {

        }
    }
}
