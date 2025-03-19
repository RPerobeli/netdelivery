using Microsoft.EntityFrameworkCore;
using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> Add(Customer customer);
        public bool Update(Customer customer);
        public Task<List<Customer>> GetAll();
        public Task<Customer> GetById(int id);
        public Task<List<Customer>> GetByType(EUserType uType);
        public Task Commit();
    }
    public class UserRepository : IUserRepository
    {
        private readonly NetDeliveryContext _context;

        public UserRepository(NetDeliveryContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Customer customer)
        {
            //retornar o id ao inves do bool
            var retorno = await _context.AddAsync(customer);
            if (retorno != null)
                return true;
            else
                return false;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAll()
        {
            var list = await _context.Usuarios.ToListAsync();
            return list;
        }

        public async Task<Customer> GetById(int id)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
            return user;
        }

        public async Task<List<Customer>> GetByType(EUserType uType)
        {
            var user = (await _context.Usuarios.ToListAsync()).Where(c => c.Type == uType).ToList();
            return user;
        }

        public  bool Update(Customer customer)
        {
            var retorno = _context.Update(customer);
            if(retorno != null) return true;
            else return false;
        }
    }
}
