using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using BWC.Core.Interfaces;
using Dapper;

namespace BWC.Core.Repositories
{
    public class ProductComponentRepository:BaseRepository,IProductComponent
    {
        public IEnumerable<ProductComponent> GetAll(int productId)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<ProductComponent>(@"
                    sp_GetAllProductComponent @ProductId=@productId
                ", new { productId }).ToList();
            }
        }

        public IEnumerable<ProductComponent> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductComponent GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<ProductComponent>(@"
                    sp_GetProductComponent @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(ProductComponent entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_InsertProductComponent 
                                    @ProductId=@productId,
                                    @ComponentId=@ComponentId,
                                    @ColorId=@ColorId,
                                    @Quantity=@Quantity,
                                    @Price=@Price,
                                    @ExtCharge=@ExtCharge
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
                    exec sp_DeleteProductComponent @Id=@id", new { id });
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
                     sp_UpdateProductComponent 
                                    @Id=@Id,
                                    @ProductId=@productId,
                                    @ComponentId=@ComponentId,
                                    @ColorId=@ColorId,
                                    @Quantity=@Quantity,
                                    @Price=@Price,
                                    @ExtCharge=@ExtCharge
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
