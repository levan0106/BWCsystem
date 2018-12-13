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
    class EmployeeRepository : BaseRepository, IEmployee
    {
        public IEnumerable<Employee> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Employee>(@"
                   call sp_GetAllEmployee
                ", new { }).ToList();
            }
        }

        public Employee GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Employee>(@"
                call sp_GetEmployee(@id)
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Employee entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                call sp_InsertEmployee(
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
                                    @TaxFileNumber,
	                                @SuperannuationName,
	                                @FundABN,
	                                @FundAddress,
	                                @FundUSI,
	                                @SuperannuationMemberNumber,
                                    @SalaryPerHours,
	                                @SickLeaveRate,
	                                @AnnualLeaveRate,
                                    @HireDate,
                                    @LastWorkingDate
                            )
                ", entity);

                    return connection.Query(@"
                call sp_GetEmployee (@id)
                ", new { entity.Id }).Any();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert Employee: ", e);
                throw;
            }

        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_DeleteEmployee (@id)", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete Employee: ", e);
                    return false;
                }
            }
        }

        public bool Update(Employee entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateEmployee (
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
                                    @TaxFileNumber,
	                                @SuperannuationName,
	                                @FundABN,
	                                @FundAddress,
	                                @FundUSI,
	                                @SuperannuationMemberNumber,
                                    @SalaryPerHours,
	                                @SickLeaveRate,
	                                @AnnualLeaveRate,
                                    @HireDate,
                                    @LastWorkingDate
                                )
                    ", entity);
                    return true;
                }
            }
            catch(Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update Employee: ", e);
                return false;
            }

        }
    }
}
