using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Enums;
using Net.Delivery.Order.Domain.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Services
{
    public interface IUserService
    {
        public Task<bool> Add(Customer customer);
        public bool Update(Customer customer);
        public Task<List<Customer>> GetAll();
        public Task<Customer> GetById(int id);
        public Task<List<Customer>> GetByType(EUserType uType);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Add(Customer customer)
        {
            //TODO: ideias -> validar campos baseado no tipo de consumidor, tratar o mapeamento de DTO para Model aqui
            if(customer is not null)
            {
                return await _userRepository.Add(customer);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<List<Customer>> GetByType(EUserType uType)
        {
            return await _userRepository.GetByType(uType);
        }

        public bool Update(Customer customer)
        {
            if(customer is not null)
            {
                return _userRepository.Update(customer);
            }else { return false; }
        }
    }
}
