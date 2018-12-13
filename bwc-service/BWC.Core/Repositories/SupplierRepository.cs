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
    class SupplierRepository:BaseRepository, ISupplier
    {
        public IEnumerable<Supplier> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Supplier>(@"
                    sp_GetAllSupplier
                ", new {}).ToList();
            }
        }

        public Supplier GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Supplier>(@"
                sp_GetSupplier @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Supplier entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                sp_InsertSupplier @FirstName=@FirstName,
                                    @LastName=@LastName,
                                    @Company=@Company,
                                    @Email=@Email,
                                    @JobTitle=@JobTitle,
                                    @WebPage=@WebPage,
                                    @Notes=@Notes,
                                    @Address=@Address,
                                    @ZipCode=@ZipCode,
                                    @City=@City,
                                    @State=@State,
                                    @Country=@Country,
                                    @BusinessPhone=@BusinessPhone,
                                    @MobilePhone=@MobilePhone,
                                    @HomePhone=@HomePhone,
                                    @Fax=@Fax,
                                    @ABN=@ABN
                ", entity);

                return connection.Query(@"
                sp_GetSupplier @Id=@id
                ", new { entity.Id }).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    sp_DeleteSupplier @Id=@id", new { id });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Update(Supplier entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateSupplier 
                                    @Id=@Id,
                                    @FirstName=@FirstName,
                                    @LastName=@LastName,
                                    @Company=@Company,
                                    @Email=@Email,
                                    @JobTitle=@JobTitle,
                                    @WebPage=@WebPage,
                                    @Notes=@Notes,
                                    @Address=@Address,
                                    @ZipCode=@ZipCode,
                                    @City=@City,
                                    @State=@State,
                                    @Country=@Country,
                                    @BusinessPhone=@BusinessPhone,
                                    @MobilePhone=@MobilePhone,
                                    @HomePhone=@HomePhone,
                                    @Fax=@Fax,
                                    @ABN=@ABN
                    ", entity);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
    }
}
