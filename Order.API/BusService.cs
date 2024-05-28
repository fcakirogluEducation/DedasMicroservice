using System.Text;
using System.Text.Json;
using Bus.Shared;
using RabbitMQ.Client;

namespace Order.API
{
    public interface IBusService
    {
        void SendOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent);
    }


    public class BusService(IModel channel) : IBusService
    {
        public void SendOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent)
        {
            var body = JsonSerializer.Serialize(orderCreatedEvent);
            var bodyBytes = Encoding.UTF8.GetBytes(body);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(BusConst.OrderCreatedEventExchange, string.Empty, properties, bodyBytes);
            channel.WaitForConfirms(TimeSpan.FromSeconds(5));
        }
    }
}