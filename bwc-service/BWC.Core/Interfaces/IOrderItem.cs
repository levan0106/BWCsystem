using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;

namespace BWC.Core.Interfaces
{
    public interface IOrderItem:IRepository<OrderItem>
    {
        IEnumerable<OrderItem> GetAll(int orderType);
    }
}
