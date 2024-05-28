using Bus.Shared;

namespace Order.API
{
    public class OrderService(IBusService busService)
    {
        public void OrderCrete()
        {
            // save to db

            // send event
            var orderCreatedEvent =
                new OrderCreatedEvent(Guid.NewGuid().ToString(), DateTime.Now, 2, 100,
                [
                    new(1, 1),
                    new(2, 2)
                ]);

            busService.SendOrderCreatedEvent(orderCreatedEvent);
        }
    }
}