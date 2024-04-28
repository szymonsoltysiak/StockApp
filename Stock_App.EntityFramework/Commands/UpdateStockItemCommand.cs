using Stock_App.Domain.Commands;
using Stock_App.Domain.Model;
using Stock_App.EntityFramework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.EntityFramework.Commands
{
    public class UpdateStockItemCommand : IUpdateStockItemCommand
    {
        private readonly StockAppDbContextFactory _contextFactory;

        public UpdateStockItemCommand(StockAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(StockItem itemStock)
        {
            using (StockAppDbContext context = _contextFactory.Create())
            {
                StockItemDto stockItemDto = new StockItemDto()
                {
                    Ticker = itemStock.Ticker,
                    Price = itemStock.Price,
                };
                context.StockItems.Update(stockItemDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
