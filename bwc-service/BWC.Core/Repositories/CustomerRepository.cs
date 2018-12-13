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
    class CustomerRepository:BaseRepository, ICustomer
    {
        public IEnumerable<Customer> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Customer>(@"
                    sp_GetAllCustomer
                ", new {}).ToList();
            }
        }

        public Customer GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Customer>(@"
                sp_GetCustomer @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Customer entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                sp_InsertCustomer @FirstName=@FirstName,
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
                                    @Discount=@Discount,
                                    @ABN=@ABN
                ", entity);

                return connection.Query(@"
                sp_GetCustomer @Id=@id
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
                    sp_DeleteCustomer @Id=@id", new { id });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Update(Customer entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateCustomer 
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
                                    @Discount=@Discount,
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

        public Customer GetSystemInfo()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Customer>(@"
                    sp_GetSystemInfo
                    ", new { }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

                BWC.Core.Common.LogManager.LogError("get system info: ", e);
                return null;
            }
        }
    }
}
