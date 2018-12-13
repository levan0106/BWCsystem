using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using BWC.Core.Interfaces;
using Dapper;

namespace BWC.Core.Repositories.MySql
{
    public class ProductMaterialRepository : BaseRepository, IProductMaterial
    {

        public IEnumerable<ProductMaterial> GetAll(int productId)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<ProductMaterial>(@"
                  call  sp_GetAllProductMaterial (@productId)
                ", new { productId }).ToList();
            }
        }
        public IEnumerable<ProductMaterial> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductMaterial GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ProductMaterial entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_InsertProductMaterial (
                                    @ProductId,
                                    @MaterialId
                            )
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert Material into product: ", e);
                    return false;
                }
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_DeleteProductMaterial (@id)", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete a material in product: ", e);
                    return false;
                }
            }
        }

        public bool Update(ProductMaterial entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}
