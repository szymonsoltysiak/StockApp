using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.EntityFramework
{
    public class StockItemsDbContextFactory
    {
        private readonly DbContextOptions _options;

        public StockItemsDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public StockAppDbContext Create()
        {
            return new StockAppDbContext(_options);
        }
    }
}
