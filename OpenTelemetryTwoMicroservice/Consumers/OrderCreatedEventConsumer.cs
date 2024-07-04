using Automatonymous;
using Bus.Shared;
using MassTransit;
using Activity = System.Diagnostics.Activity;

namespace OpenTelemetryTwoMicroservice.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine($"order Id:{context.Message.OrderId}");

            await Task.Delay(2000);

            Activity.Current?.SetTag("custom.order.id", context.Message.OrderId);
        }
    }
}