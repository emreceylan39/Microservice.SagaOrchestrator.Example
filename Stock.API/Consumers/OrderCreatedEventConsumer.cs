using MassTransit;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using Shared.OrderEvents;
using Shared.Settings;
using Shared.StockEvents;
using Stock.API.Contexts;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private StockContext _stockDbContext;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderCreatedEventConsumer(StockContext stockDbContext, ISendEndpointProvider sendEndpointProvider)
        {
            _stockDbContext = stockDbContext;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            List<bool> stockResults = new();

            foreach (var orderItem in context.Message.OrderItems)
            {
                stockResults.Add(await _stockDbContext.Stocks.Where(s => s.ProductId == orderItem.ProductId && s.Count >= orderItem.Count).AnyAsync());
            }

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.StateMachineQueue}"));

            if (stockResults.TrueForAll(s => s.Equals(true)))
            {
                foreach (var orderItem in context.Message.OrderItems)
                {
                    var stock = await _stockDbContext.Stocks.Where(s => s.ProductId == orderItem.ProductId).FirstOrDefaultAsync();
                    stock.Count -= orderItem.Count;

                }
                await _stockDbContext.SaveChangesAsync();

                StockReservedEvent stockReservedEvent = new(context.Message.CorrelationId)
                {
                    OrderItems = context.Message.OrderItems,
                };

                await sendEndpoint.Send(stockReservedEvent);
            }
            else
            {
                StockNotReservedEvent stockNotReservedEvent = new(context.Message.CorrelationId)
                {
                    Message = "Stok yetersiz.."
                };

                await sendEndpoint.Send(stockNotReservedEvent);
            }
        }
    }
}
