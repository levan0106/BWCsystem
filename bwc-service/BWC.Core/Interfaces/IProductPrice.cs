using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Core.Interfaces
{
    public interface IProductPrice:IRepository<ProductPrice>
    {
        IEnumerable<ProductPrice> GetAllByProductMaterial(int productMaterialId, int? priceType);
        IEnumerable<ProductPrice> GetAllByProduct(int productId, int? priceType);
        IEnumerable<ProductPrice> GetListGroupPriceByProduct(int productMaterialId, int? priceType);
        IEnumerable<ProductPrice> AllPriceByGroup(object groupId, int? priceType);        
        bool Update(int productPriceId, object groupId, string values, int? priceType, string userLogin);
        bool CopyPrice(int productMaterialId, int? priceType, string userLogin);
    }
}
