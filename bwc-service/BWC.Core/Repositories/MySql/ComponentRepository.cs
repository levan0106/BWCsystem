using BWC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using Dapper;

namespace BWC.Core.Repositories.MySql
{
    public class ComponentRepository:BaseRepository, IComponent
    {
        public IEnumerable<Component> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Component>(@"
                    call sp_GetAllComponent
                ", new { }).ToList();
            }
        }

        public IEnumerable<Component> GetAllBySupplier(int id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Component>(@"
                    call sp_GetAllComponentBySupplier(@id)
                ", new { id }).ToList();
            }
        }

        public Component GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Component>(@"
                call sp_GetComponent(@id)
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
                    call sp_InsertComponent(
                                        @ComponentCode,
                                        @ComponentName,
                                        @SupplierId,
                                        @Price,
                                        @PurchasePrice,
                                        @Color,
                                        @Unit,
                                        @Description,
                                        @Discount,
                                        @ActiveStatus
                                    )
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
                    call sp_DeleteComponent(@id)
                    ", new { id });
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
                    call sp_UpdateComponent (
                                    @Id,
                                    @ComponentCode,
                                    @ComponentName,
                                    @SupplierId,
                                    @Price,
                                    @PurchasePrice,
                                    @Color,
                                    @Unit,
                                    @Description,
                                    @Discount,
                                    @ActiveStatus
                            )
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
