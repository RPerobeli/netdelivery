using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Net.Delivery.Order.Domain.Entities;
using Net.Delivery.Order.Domain.Services;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Notifier
{
    public class WorkerSMS : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _orderTopicName;
        private readonly string _kafkaBootstrapServers;
        private readonly string _notifierConsumeGroupName;

        public WorkerSMS(ILogger<Worker> logger, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _orderTopicName = _configuration["OrderSettings:OrderTopicName"];
            _kafkaBootstrapServers = _configuration["OrderSettings:KafkaBootstrapServer"];
            _notifierConsumeGroupName = _configuration["OrderSettings:SmsNotifierConsumeGroupName"];
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Lopping process
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            INotifierService notifierService = new NotifierService();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Order notifier running at: {time}", DateTimeOffset.Now);

                var config = new ConsumerConfig
                {
                    BootstrapServers = _kafkaBootstrapServers,
                    GroupId = _notifierConsumeGroupName,
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(_orderTopicName);

                    CancellationTokenSource cts = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) => {
                        e.Cancel = true;
                        cts.Cancel();
                    };

                    try
                    {
                        while (true)
                        {
                            try
                            {
                                var consumeResult = consumer.Consume(cts.Token);

                                Domain.Entities.Order order = JsonSerializer.Deserialize<Domain.Entities.Order>(consumeResult.Message.Value);

                                using (var scope = _serviceProvider.CreateScope())
                                {
                                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                                    Customer customer = await userService.GetById((int)order.CustomerId);
                                    notifierService.NotifySMS(customer);
                                }

                                //Console.WriteLine($"WORKER-SMS Mensagem recebida: {consumeResult.Message.Value}");
                            }
                            catch (ConsumeException e)
                            {
                                Console.WriteLine($"WORKER-SMS Erro ao consumir a mensagem: {e.Error.Reason}");
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                    }
                }
            }
        }
    }
}
