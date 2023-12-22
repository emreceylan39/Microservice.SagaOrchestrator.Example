using MassTransit;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.StockEvents
{
    public class StockReservedEvents : CorrelatedBy<Guid>
    {
        public StockReservedEvents(Guid correlatedId)
        {
            CorrelationId = correlatedId;
        }
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
