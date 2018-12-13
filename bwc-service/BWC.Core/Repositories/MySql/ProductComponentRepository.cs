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
    public class ProductComponentRepository:BaseRepository,IProductComponent
    {
        public IEnumerable<ProductComponent> GetAll(int productId)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<ProductComponent>(@"
                   call sp_GetAllProductComponent(@productId)
                ", new { productId }).ToList();
            }
        }

        public IEnumerable<ProductComponent> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductComponent GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<ProductComponent>(@"
                   call sp_GetProductComponent (@id)
                ", new { id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Product get info: ", e);
                return new ProductComponent();
            }
            
        }

        public bool Insert(ProductComponent entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_InsertProductComponent (
                                    @productId,
                                    @ComponentId,
                                    @ColorId,
                                    @Quantity,
                                    @Price,
                                    @ExtCharge
                                )
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert component into product: ", e);
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
                    call sp_DeleteProductComponent (@id)", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete a component of product: ", e);
                    return false;
                }
            }
        }

        public bool Update(ProductComponent entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_UpdateProductComponent (
                                    @Id,
                                    @productId,
                                    @ComponentId,
                                    @ColorId,
                                    @Quantity,
                                    @Price,
                                    @ExtCharge
                                )
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update component info in product: ", e);
                    return false;
                }
            }
        }
    }
}
