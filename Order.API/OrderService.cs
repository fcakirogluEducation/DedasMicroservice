using System.Text.Json;
using Bus.Shared;
using MassTransit;
using Order.API.Models;
using OrderItem = Order.API.Models.OrderItem;

namespace Order.API
{
    public class OrderService(IBusService busService, AppDbContext contex, IPublishEndpoint publishEndpoint)
    {
        public async Task OrderCrete()
        {
            await using var transaction = await contex.Database.BeginTransactionAsync();
            var order = new Models.Order
            {
                Created = DateTime.Now,
                UserId = 10,
                OrderItems =
                [
                    new OrderItem() { ProductId = 1, Count = 1 },
                    new OrderItem() { ProductId = 2, Count = 2 }
                ]
            };

            contex.Orders.Add(order);
            await contex.SaveChangesAsync();


            // save to db
            // send event
            var orderCreatedEvent =
                new OrderCreatedEvent(order.Id.ToString(), DateTime.Now, 2, 100,
                [
                    new(1, 1),
                    new(2, 2)
                ]);


            var outbox = new OutBox
            {
                Created = DateTime.Now,
                EventType = nameof(OrderCreatedEvent),
                PayloadEvent = JsonSerializer.Serialize(orderCreatedEvent),
                SendBus = false
            };


            contex.OutBoxes.Add(outbox);
            await contex.SaveChangesAsync();

            await transaction.CommitAsync();


            await publishEndpoint.Publish(orderCreatedEvent);


            //try
            //{
            //    busService.SendOrderCreatedEvent(orderCreatedEvent);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }


        public async Task OrderCreate2()
        {
            var orderCreatedEvent =
                new OrderCreatedEvent(Guid.NewGuid().ToString(), DateTime.Now, 2, 100,
                [
                    new(1, 1),
                    new(2, 2)
                ]);
            await publishEndpoint.Publish(orderCreatedEvent);
        }
    }
}