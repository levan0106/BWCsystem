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
    class SupplierRepository:BaseRepository, ISupplier
    {
        public IEnumerable<Supplier> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Supplier>(@"
                   call sp_GetAllSupplier
                ", new {}).ToList();
            }
        }

        public Supplier GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Supplier>(@"
                call sp_GetSupplier (@id)
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Supplier entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_InsertSupplier (
                                    @FirstName,
                                    @LastName,
                                    @Company,
                                    @Email,
                                    @JobTitle,
                                    @WebPage,
                                    @Notes,
                                    @Address,
                                    @ZipCode,
                                    @City,
                                    @State,
                                    @Country,
                                    @BusinessPhone,
                                    @MobilePhone,
                                    @HomePhone,
                                    @Fax,
                                    @ABN
                                )
                ", entity);

                    return connection.Query(@"
                call sp_GetSupplier (@id)
                ", new { entity.Id }).Any();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert Supplier: ", e);
                return false;
            }
            
        }

        public bool Delete(object id, string userLogin)
        {

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                call sp_DeleteSupplier (@id)", new { id });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Delete Supplier: ", e);
                return false;
            }
        }

        public bool Update(Supplier entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_UpdateSupplier (
                                    @Id,
                                    @FirstName,
                                    @LastName,
                                    @Company,
                                    @Email,
                                    @JobTitle,
                                    @WebPage,
                                    @Notes,
                                    @Address,
                                    @ZipCode,
                                    @City,
                                    @State,
                                    @Country,
                                    @BusinessPhone,
                                    @MobilePhone,
                                    @HomePhone,
                                    @Fax,
                                    @ABN
                                )
                    ", entity);
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Supplier: ", e);
                    return false;
                }

            }
        }
    }
}
