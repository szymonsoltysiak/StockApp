using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.EntityFramework
{
    public class StockAppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<StockAppDbContext>
    {
        public StockAppDbContext CreateDbContext(string[] args = null)
        {
            return new StockAppDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=StockApp.db").Options);
        }
    }
}
