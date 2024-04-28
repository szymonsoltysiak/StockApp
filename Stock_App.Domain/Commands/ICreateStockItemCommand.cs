using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock_App.Domain.Model;

namespace Stock_App.Domain.Commands
{
    public interface ICreateStockItemCommand
    {
        Task Execute(StockItem itemStock);
    }
}
