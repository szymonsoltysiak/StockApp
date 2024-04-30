using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock_App.Domain.Model;
using Stock_App.Domain.Queries;
using Stock_App.EntityFramework.DTOs;

namespace Stock_App.EntityFramework.Queries
{
    public class GetAllStockItemsQuery : IGetAllStockItemsQuery
    {
        private readonly StockAppDbContextFactory _contextFactory;

        public GetAllStockItemsQuery(StockAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<StockItem>> Execute()
        {
            using(StockAppDbContext context = _contextFactory.Create())
            {
                IEnumerable<StockItemDto> stockItemDtos = await context.StockItems.ToListAsync();

                return stockItemDtos.Select(x => new StockItem(x.Id, x.Ticker, x.Price));
            }
        }
    }
}
