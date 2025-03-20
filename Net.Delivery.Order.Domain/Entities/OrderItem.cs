using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Entities
{
    public class OrderItem
    {
        public string OrderId { get; set; } // Chave estrangeira para Pedido
        public long ItemId { get; set; }  // Chave estrangeira para Item

        // Propriedades de navegação
        public Entities.Order Order { get; set; }
        public Item Item { get; set; }
    }
}
