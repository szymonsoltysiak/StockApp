using Stock_App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.Domain.Commands
{
    public interface IUpdateStockItemCommand
    {
        Task Execute(StockItem itemStock);
    }
}
