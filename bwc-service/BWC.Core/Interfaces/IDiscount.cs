using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Core.Interfaces
{
    public interface IDiscount:IRepository<Discount>
    {
        IEnumerable<Discount> GetAll(int discountType);
    }
}
