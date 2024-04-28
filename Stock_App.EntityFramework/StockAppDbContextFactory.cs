using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.EntityFramework
{
    public class StockAppDbContextFactory
    {
        private readonly DbContextOptions _options;

        public StockAppDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public StockAppDbContext Create()
        {
            return new StockAppDbContext(_options);
        }
    }
}
