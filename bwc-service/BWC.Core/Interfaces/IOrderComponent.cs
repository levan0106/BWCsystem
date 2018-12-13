using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;

namespace BWC.Core.Interfaces
{
    public interface IOrderComponent : IRepository<OrderComponent>
    {
        IEnumerable<OrderComponent> GetAll(object orderId);
        bool Update(Int64 orderId, string values, string userLogin);
        bool AddFromOrder( object orderId, string values, string userLogin);
        bool MarkToComplete(string values, string userLogin);
    }
}
