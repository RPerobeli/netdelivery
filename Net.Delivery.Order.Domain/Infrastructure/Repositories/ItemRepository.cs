using Microsoft.EntityFrameworkCore;
using Net.Delivery.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Infrastructure.Repositories
{
    public interface IItemRepository
    {
        public Task<bool> Add(Item customer);
        public bool Update(Item customer);
        public Task<List<Item>> GetAll();
        public Task<Item> GetById(int id);
        public Task Commit();
    }
    public class ItemRepository : IItemRepository
    {
        private readonly NetDeliveryContext _context;

        public ItemRepository(NetDeliveryContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Item item)
        {
            var retorno = await _context.AddAsync(item);
            if (retorno != null)
                return true;
            else
                return false;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Item>> GetAll()
        {
            var list = await _context.Items.ToListAsync();
            return list;
        }

        public async Task<Item> GetById(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(c => c.ItemId == id);
            return item;
        }

        public  bool Update(Item item)
        {
            var retorno = _context.Update(item);
            if(retorno != null) return true;
            else return false;
        }
    }
}
