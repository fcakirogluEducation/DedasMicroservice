using Bus.Shared;
using MassTransit;

namespace Stock.API.Consumers
{
    public class PaymentStartingMessageConsumer(IPublishEndpoint publishEndpoint, IServiceProvider serviceProvider)
        : IConsumer<PaymentStartingMessage>
    {
        public Task Consume(ConsumeContext<PaymentStartingMessage> context)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var logger = serviceProvider.GetService<ILogger<PaymentStartingMessageConsumer>>();


                // db.save changes
            }


            //redis

            // publishEndpoint.publish
            // publishEndpoint.publish


            Console.WriteLine($"payment :{context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}