using System.Text;
using System.Text.Json;
using Bus.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer(IConnection connection, ILogger<OrderCreatedEventConsumer> logger)
        : BackgroundService
    {
        private IModel? _channel;
        private EventingBasicConsumer? _eventingBasicConsumer;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = connection.CreateModel();

            _channel.QueueDeclare(BusConst.StockOrderCreatedEventQueue, true, true, false, null);

            _channel.QueueBind(BusConst.StockOrderCreatedEventQueue, BusConst.OrderCreatedEventExchange, string.Empty,
                null);
            _eventingBasicConsumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume(BusConst.StockOrderCreatedEventQueue, false, _eventingBasicConsumer);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Close();
            _channel!.Close();
            return base.StopAsync(cancellationToken);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _eventingBasicConsumer!.Received += (sender, args) =>
            {
                var messageAsByte = Encoding.UTF8.GetString(args.Body.ToArray());

                var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(messageAsByte);


                logger.LogInformation($"stock reserved. {orderCreatedEvent!.OrderId}");
            };

            return Task.CompletedTask;
            //while (!stoppingToken.IsCancellationRequested)
            //{

            //}
        }
    }
}