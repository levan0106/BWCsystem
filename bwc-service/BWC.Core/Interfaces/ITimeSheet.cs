using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Core.Interfaces
{
    public interface ITimeSheet:IRepository<TimeSheet>
    {
        IEnumerable<TimeSheet> GetAllTimeSheet(object fromDate, object toDate,bool isPaid);
        IEnumerable<TimeSheet> GetAllEmployeeTimeSheet(object fromDate, object toDate);
        IEnumerable<TimeSheetDetail> GetTimeSheetDetail(object date);
        bool addTimeSheetEmployee(TimeSheet entity, string userLogin);
        bool updateTimeSheet(object date, string values, string userLogin);
        bool approveTimeSheet(object fromDate, object toDate, string userLogin);
        bool updateLessPAYG(object fromDate, object toDate, string values, string userLogin);
        bool updateLessPAYGStatus(object id,object value, string userLogin);
    }
}
