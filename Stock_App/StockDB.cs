using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Stock_App.Services;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App
{
    public class StockItemDB
    {
        public int ID { get; set; }
        public string Ticker { get; set; }
        public double Price { get; set; }

        public StockItemDB() { }

        public StockItemDB(string ticker, double price)
        {
            Ticker = ticker;
            Price = price;
        }
    }
    internal class StockDB : DbContext
    {
        public DbSet<StockItemDB> Stocks { get; set; }
        public string DbPath { get; set; }
        
        public StockDB()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "StockDatabase.db");
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
            /*{
            string pathToDatabase = @"C:\Users\szymo\source\repos\Stock_App\Stock_App\StockDatabase.db";
            optionsBuilder.UseSqlite($"DataSource={pathToDatabase}");
        }*/
    }
}
