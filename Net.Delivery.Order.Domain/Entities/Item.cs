using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Entities
{
    public class Item
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitValue { get; set; }




        //Relacionamentos
        public ICollection<OrderItem> OrderItens { get; set; } = new List<OrderItem>();
    }
}
