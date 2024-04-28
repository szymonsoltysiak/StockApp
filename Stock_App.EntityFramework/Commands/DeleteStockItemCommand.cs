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
    public class DeleteStockItemCommand : IDeleteStockItemCommand
    {
        private readonly StockAppDbContextFactory _contextFactory;

        public DeleteStockItemCommand(StockAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(string ticker)
        {
            using (StockAppDbContext context = _contextFactory.Create())
            {
                StockItemDto stockItemDto = new StockItemDto()
                {
                    Ticker = ticker,
                };
                context.StockItems.Remove(stockItemDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
