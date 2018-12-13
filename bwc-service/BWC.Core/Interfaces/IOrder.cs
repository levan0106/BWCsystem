using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;

namespace BWC.Core.Interfaces
{
    public interface IOrder:IRepository<Order>
    {
        IEnumerable<Order> GetAll(int orderType);
        IEnumerable<Order> GetAllByDateRange(int orderType, object from, object to);
        bool Copy(object id, object newId, string userLogin);
    }
}
