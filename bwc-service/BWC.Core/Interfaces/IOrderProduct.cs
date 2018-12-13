using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;

namespace BWC.Core.Interfaces
{
    public interface IOrderProduct : IRepository<OrderProduct>
    {
        IEnumerable<OrderProduct> GetAll(object orderId);
        bool AddFromOrder(object orderId, string values, string userLogin);
        bool MarkToComplete(string values, string userLogin);
        bool UpdateProductStep(object orderId, int step, string userLogin);
    }
}
