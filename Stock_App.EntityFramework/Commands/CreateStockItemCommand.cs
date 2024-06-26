﻿using Microsoft.EntityFrameworkCore;
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
    public class CreateStockItemCommand : ICreateStockItemCommand
    {
        private readonly StockAppDbContextFactory _contextFactory;

        public CreateStockItemCommand(StockAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(StockItem stockItem)
        {
            using (StockAppDbContext context = _contextFactory.Create())
            {
                StockItemDto stockItemDto = new StockItemDto()
                {
                    Id = stockItem.Id,
                    Ticker = stockItem.Ticker,
                    Price = stockItem.Price,
                };
                context.StockItems.Add(stockItemDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
