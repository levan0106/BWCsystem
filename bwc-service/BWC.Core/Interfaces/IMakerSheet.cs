using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Core.Interfaces
{
    public interface IMakerSheet:IRepository<MakerSheet>
    {
        IEnumerable<MakerSheet> GetAllProducts(object id);
        IEnumerable<MakerSheetComponent> GetAllProductComponents(object id);
        IEnumerable<MakerSheetComponent> GetAllComponents(object id);
    }
}
