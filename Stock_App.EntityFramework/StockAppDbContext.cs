using Microsoft.EntityFrameworkCore;
using Stock_App.EntityFramework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.EntityFramework
{
    public class StockAppDbContext : DbContext
    {
        public StockAppDbContext(DbContextOptions options) : base(options){}

        public DbSet<StockItemDto> StockItems {  get; set; }
    }
}
