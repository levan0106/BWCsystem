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
    public class TimeSheetRepository:BaseRepository, ITimeSheet
    {
        public bool addTimeSheetEmployee(TimeSheet entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_InsertTimeSheetEmployee(
                                                @month, 
                                                @EmployeeId
                                                )
                    ",  entity );
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert Time Sheet Employee: ", e);
                return false;
            }
        }

        public bool updateTimeSheet(object date, string values, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateTimeSheet(
                                        @date, 
                                        @values
                                        )
                    ", new { date , values });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update Time Sheet: ", e);
                return false;
            }
        }

        public IEnumerable<TimeSheet> GetAllTimeSheet(object fromDate, object toDate, bool isPaid)
        {

            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<TimeSheet>(@"
                    call sp_GetAllTimeSheet(@fromDate,@toDate,@isPaid)
                    ", new { fromDate,toDate, isPaid }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All Time Sheet: ", e);
                return null;
            }
        }

        public IEnumerable<TimeSheet> GetAllEmployeeTimeSheet(object fromDate, object toDate)
        {

            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<TimeSheet>(@"
                    call sp_GetAllEmployeeTimeSheet(@fromDate,@toDate)
                    ", new { fromDate, toDate }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All Time Sheet: ", e);
                return null;
            }
        }

        public IEnumerable<TimeSheetDetail> GetTimeSheetDetail(object date)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<TimeSheetDetail>(@"
                    call sp_GetTimeSheetDetail(@date)
                    ", new { date }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get Time Sheet Detail: ", e);
                return null;
            }
        }
        public bool approveTimeSheet(object fromDate, object toDate, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_ApproveTimeSheet(@fromDate,@toDate)
                    ", new { fromDate,toDate });
                }
                return true;
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Approved Time Sheet: ", e);
                return false;
            }
        }

        public bool updateLessPAYG(object fromDate, object toDate, string values, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateLessPAYG(
                                        @fromDate,
                                        @toDate,
                                        @values
                                        )
                    ", new { fromDate, toDate, values });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update Less Payg: ", e);
                return false;
            }
        }

        public bool updateLessPAYGStatus(object id, object value, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateLessPAYGStatus(
                                        @id,
                                        @value
                                        )
                    ", new { id, value });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update Less PAYG status: ", e);
                return false;
            }
        }
        public TimeSheet GetInfo(object id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSheet> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(TimeSheet entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(TimeSheet entity, string userLogin)
        {
            throw new NotImplementedException();
        }

    }
}
