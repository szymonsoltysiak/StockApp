using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Stock_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App
{
    public class StockItemDB
    {
        public int ID;
        public string Ticker;
        public double Price;

        public StockItemDB(int iD, string ticker, double price)
        {
            ID = iD;
            Ticker = ticker;
            Price = price;
        }
    }
    internal class StockDB : DbContext
    {
        public DbSet<StockItemDB> Stocks;
        
        public StockDB()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string pathToDatabase = @"C:\Users\szymo\source\repos\Stock_App\Stock_App\StockDatabase.db";
            optionsBuilder.UseSqlite($"DataSource={pathToDatabase}");
        }
    }
}
