using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.EntityFramework.DTOs
{
    public class StockItemDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public double Price { get; set; }
    }
}
