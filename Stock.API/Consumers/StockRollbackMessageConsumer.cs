using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;
using Stock.API.Contexts;

namespace Stock.API.Consumers
{
    public class StockRollbackMessageConsumer : IConsumer<StockRollbackMessage>
    {
        private StockContext _stockContext;

        public StockRollbackMessageConsumer(StockContext stockContext)
        {
            _stockContext = stockContext;
        }

        public async Task Consume(ConsumeContext<StockRollbackMessage> context)
        {
            foreach(var orderItem in context.Message.OrderItems)
            {
                var stock = await _stockContext.Stocks.Where(s => s.ProductId == orderItem.ProductId).FirstOrDefaultAsync();

                stock.Count += orderItem.Count;
            }

            await _stockContext.SaveChangesAsync();
        }
    }
}
