using Confluent.Kafka;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using Net.Delivery.Order.Domain.Infrastructure.Repositories;
using Net.Delivery.Order.Domain.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Net.Delivery.Order.Domain.Model;

namespace Net.Delivery.Order.Domain.Services
{
    /// <summary>
    /// Order service
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IConfiguration _configuration;
        private readonly string _orderTopicName;
        private readonly string _kafkaBootstrapServers;

        /// <summary>
        /// Order service builder
        /// </summary>
        /// <param name="orderRepository">Order repository</param>
        /// <param name="configuration">Global configuration (It is being used the "appsettings.json" file located in the solution's root</param>
        public OrderService(IConfiguration configuration, IOrderRepository orderRepository, IUserRepository userRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            _orderTopicName = _configuration["OrderSettings:OrderTopicName"];
            _kafkaBootstrapServers = _configuration["OrderSettings:KafkaBootstrapServer"];
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// Creates a order
        /// </summary>
        /// <param name="items">Order items</param>
        /// <param name="customer">Order customer</param>
        public async Task<string> CreateOrder(IList<long> items, long customerId)
        {
            var cs = _configuration.GetConnectionString("DefaultConnection");

            Customer customer = await _userRepository.GetById((int)customerId);
            if(customer.Type == Enums.EUserType.EMPRESA)
            {
                throw new InvalidOperationException("The user must be a client");
            }

            Entities.Order order = new Entities.Order(customer);

            List<Item> itemsList = new List<Item>();
            foreach (var itemId in items)
            {
                Item item = await _itemRepository.GetById((int)itemId);
                if (item != null)
                {
                    itemsList.Add(item);
                }
            }
            order.Items = itemsList;
            string orderId = _orderRepository.Add(order, itemsList);

            DeliveryResult<Null, string> deliveryReport = await PublishMessageToTopic(order);

            if(!string.IsNullOrEmpty(orderId))
            {
                return orderId;
            }else 
            { 
                return null;
            }
        }

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <param name="orderId">Order identification</param>
        /// <param name="orderSituation">Order situation</param>
        public async Task UpdateOrderSituation(string orderId, OrderSituation orderSituation)
        {
            Entities.Order orderToBeUpdated = _orderRepository.GetOrderById(orderId);

            orderToBeUpdated.OrderSituation = orderSituation;
            orderToBeUpdated.OrderLastUpdate = DateTime.Now;

            _orderRepository.Update(orderToBeUpdated);

            await PublishMessageToTopic(orderToBeUpdated);
        }

        /// <summary>
        /// Gets all orders to delivery
        /// </summary>
        public IList<Entities.Order> GetAllOrdersToDelivery()
        {
            return _orderRepository.GetOrdersBySituation(OrderSituation.CREATED);
        }

        /// <summary>
        /// Publishs message with order data to Kafka topic
        /// </summary>
        /// <param name="order">Order data</param>
        /// <returns></returns>
        private async Task<DeliveryResult<Null, string>> PublishMessageToTopic(Entities.Order order)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaBootstrapServers
            };

            string orderConvertedToJson = JsonSerializer.Serialize(order);

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                DeliveryResult<Null, string> deliveryReport = await producer.ProduceAsync(_orderTopicName, new Message<Null, string> { Value = orderConvertedToJson });
                return deliveryReport;
            }
        }
    }
}
