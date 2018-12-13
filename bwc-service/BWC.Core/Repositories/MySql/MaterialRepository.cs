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
    public class MaterialRepository:BaseRepository,IMaterial
    {
        public IEnumerable<Material> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Material>(@"
                  call  sp_GetAllMaterial
                ", new { }).ToList();
            }
        }

        public Material GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Material>(@"
                call sp_GetMaterial (@id)
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Material entity, string userLogin)
        {
            using (var connection = GetConnection())
            {

                try
                {
                    connection.Execute(@"
                    call sp_InsertMaterial (
                                        @MaterialCode,
                                        @MaterialName, 
                                        @Color,    
                                        @SupplierId,
                                        @Price,
                                        @Description,                               
                                        @ActiveStatus
                                    )
                    ", entity);
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert Material: ", e);
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
                    call sp_DeleteMaterial (@id)", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete Material: ", e);
                    return false;
                }
            }
        }

        public bool Update(Material entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_UpdateMaterial(
                                    @Id,
                                    @MaterialCode,
                                    @MaterialName,
                                    @Color,
                                    @SupplierId,
                                    @Price,
                                    @Description,
                                    @ActiveStatus
                                )
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Material: ", e);
                    return false;
                }

            }
        }
    }
}
