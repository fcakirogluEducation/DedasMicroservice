using Bus.Shared;
using MassTransit;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumerWithMassTransit(
        ILogger<OrderCreatedEventConsumerWithMassTransit> logger,
        IServiceProvider serviceScope) : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceScope.CreateScope();
            // scope.ServiceProvider.GetRequiredService<AppDbContext>()

            logger.LogInformation($"{context.Message.OrderId}");

            return Task.CompletedTask;
        }
    }
}