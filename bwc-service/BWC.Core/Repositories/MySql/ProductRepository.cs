using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using Dapper;
using BWC.Core.Interfaces;

namespace BWC.Core.Repositories.MySql
{
    public class ProductRepository : BaseRepository, IProduct
    {
        public IEnumerable<Product> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                   call sp_GetAllProduct
                ", new { }).ToList();
            }
        }

        public Product GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                call sp_GetProduct (@id)
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Product entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_InsertProduct (
                                            @ProductCode,
                                             @ProductName,
                                             @CategoryId,
                                             @Notes,
                                             @ActiveStatus
                                )
                    ", entity);
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert Product: ", e);
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
                    call sp_DeleteProduct (@id)", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete Product: ", e);
                    return false;
                }
            }
        }

        public bool Update(Product entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_UpdateProduct (
                                    @Id,
                                    @ProductCode,
                                    @ProductName,
                                    @CategoryId,
                                    @Notes,
                                    @ActiveStatus
                                )
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Product: ", e);
                    return false;
                }
            }
        }

    }
}
