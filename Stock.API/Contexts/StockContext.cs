using Microsoft.EntityFrameworkCore;

namespace Stock.API.Contexts
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock.API.Models.Stock>().HasData(

                new Models.Stock
                {
                    Id = 1,
                    ProductId = 1,
                    Count = 100
                },
                new Models.Stock
                {
                    Id = 2,
                    ProductId = 2,
                    Count = 100
                },
                new Models.Stock
                {
                    Id = 3,
                    ProductId = 3,
                    Count = 100
                },
                new Models.Stock
                {
                    Id = 4,
                    ProductId = 4,
                    Count = 100
                }

                );
        }
    }

}
