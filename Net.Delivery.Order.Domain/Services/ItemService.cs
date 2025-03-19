using Net.Delivery.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Services
{
    public interface ItemService
    {
        public Task<bool> Add(Item customer);
        public bool Update(Item customer);
        public Task<List<Item>> GetAll();
        public Task<Item> GetById(int id);
    }
}
