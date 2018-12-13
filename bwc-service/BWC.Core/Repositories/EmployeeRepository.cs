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
    class EmployeeRepository:BaseRepository, IEmployee
    {
        public IEnumerable<Employee> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Employee>(@"
                    sp_GetAllEmployee
                ", new {}).ToList();
            }
        }

        public Employee GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Employee>(@"
                sp_GetEmployee @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Employee entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                sp_InsertEmployee @FirstName=@FirstName,
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
                                    @SalaryPerHours=@SalaryPerHours,
                                    @TaxFileNumber=@TaxFileNumber,
	                                @SuperannuationName=@SuperannuationName,
	                                @FundABN=@FundABN,
	                                @FundAddress=@FundAddress,
	                                @FundUSI=@FundUSI,
	                                @SuperannuationMemberNumber=@SuperannuationMemberNumber
                ", entity);

                return connection.Query(@"
                sp_GetEmployee @Id=@id
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
                    sp_DeleteEmployee @Id=@id", new { id });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Update(Employee entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateEmployee 
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
                                    @SalaryPerHours=@SalaryPerHours,
                                    @TaxFileNumber=@TaxFileNumber,
	                                @SuperannuationName=@SuperannuationName,
	                                @FundABN=@FundABN,
	                                @FundAddress=@FundAddress,
	                                @FundUSI=@FundUSI,
	                                @SuperannuationMemberNumber=@SuperannuationMemberNumber
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
