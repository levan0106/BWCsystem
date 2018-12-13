using BWC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using Dapper;

namespace BWC.Core.Repositories
{
    public class ComponentRepository:BaseRepository, IComponent
    {
        public IEnumerable<Component> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Component>(@"
                    sp_GetAllComponent
                ", new { }).ToList();
            }
        }

        public IEnumerable<Component> GetAllBySupplier(int id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Component>(@"
                    sp_GetAllComponentBySupplier @Id=@id
                ", new { id }).ToList();
            }
        }

        public Component GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Component>(@"
                sp_GetComponent @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Component entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    
                    connection.Execute(@"
                    sp_InsertComponent @ComponentCode=@ComponentCode,
                                        @ComponentName=@ComponentName,
                                        @SupplierId=@SupplierId,
                                        @Price=@Price,
                                        @PurchasePrice=@PurchasePrice,
                                        @Color=@Color,
                                        @Unit=@Unit,
                                        @Description=@Description,
                                        @Discount=@Discount,
                                        @ActiveStatus=@ActiveStatus
                    ", entity);

                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert Component: ", e);
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
                    exec sp_DeleteComponent @Id=@id", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete Component: ", e);
                    return false;
                }
            }
        }

        public bool Update(Component entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateComponent 
                                    @Id=@Id,
                                    @ComponentCode=@ComponentCode,
                                    @ComponentName=@ComponentName,
                                    @SupplierId=@SupplierId,
                                    @Price=@Price,
                                    @PurchasePrice=@PurchasePrice,
                                    @Color=@Color,
                                    @Unit=@Unit,
                                    @Description=@Description,
                                    @Discount=@Discount,
                                    @ActiveStatus=@ActiveStatus
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Component: ", e);
                    return false;
                }

            }
        }
    }
}
