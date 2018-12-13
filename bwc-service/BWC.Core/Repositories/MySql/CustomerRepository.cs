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
    class CustomerRepository:BaseRepository, ICustomer
    {
        public IEnumerable<Customer> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Customer>(@"
                   call sp_GetAllCustomer
                ", new {}).ToList();
            }
        }

        public Customer GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Customer>(@"
                call sp_GetCustomer (@id)
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Customer entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                call sp_InsertCustomer (
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
                                    @Discount,
                                    @ABN
                                )
                ", entity);

                return connection.Query(@"
                call sp_GetCustomer(@id)
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
                    call sp_DeleteCustomer (@id)", new { id });
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
                    call sp_UpdateCustomer (
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
                                    @Discount,
                                    @ABN
                                )
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
                    call sp_GetSystemInfo
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
