using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Shared
{
    public class BusConst
    {
        public const string OrderCreatedEventExchange = "order.created.event.exchange";
        public const string StockOrderCreatedEventQueue = "stock.order.created.event.queue";
    }
}