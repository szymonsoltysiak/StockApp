using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Stock_App
{
    public class StockItemDB
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public double Price { get; set; }

        public StockItemDB(int id, string ticker, double price) 
        {
            Id = id;
            Ticker = ticker;
            Price = price;
        }

    }


    public class StockContext : DbContext
    {
        public StockContext()
            : base("name=StockContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<StockItemDB> Stocks { get; set; }
    }

    public class StockDbInitializer : DropCreateDatabaseAlways<StockContext>
    {

        protected override void Seed(StockContext context)
        {
            var stocks = new List<StockItemDB>
                {
                    new StockItemDB(2001, "AAPL", 12.34),
                    new StockItemDB(2002, "MSFT", 34.45),
                    new StockItemDB(2003, "NVDA", 27.45)
                };
            stocks.ForEach(c => context.Stocks.Add(c));
            context.SaveChanges();

        }
    }
}
