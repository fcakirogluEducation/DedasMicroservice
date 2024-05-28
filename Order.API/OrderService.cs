using System.Text.Json;
using Bus.Shared;
using Order.API.Models;
using OrderItem = Order.API.Models.OrderItem;

namespace Order.API
{
    public class OrderService(IBusService busService, AppDbContext contex)
    {
        public void OrderCrete()
        {
            using (var transaction = contex.Database.BeginTransaction())
            {
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
                contex.SaveChanges();


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
                contex.SaveChanges();

                transaction.Commit();
            }


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
    }
}