using MassTransit;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderEvents
{
    public class OrderCreatedEvent : CorrelatedBy<Guid>
    {
        //burasını stok api tüketecek bu yüzden stock api a gereken bilgileri eklemelisin.
        public OrderCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
