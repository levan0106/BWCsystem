using BWC.Authentication.Filters;
using BWC.Core.Interfaces;
using BWC.Model;
using BWC.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BWC.Controllers
{
    [TokenAuthenticationAttribute]
    public class WorkingTimeController : BaseApiController
    {
        readonly ITimeSheet _timeSheet;
        public WorkingTimeController(ITimeSheet timeSheet)
        {
            _timeSheet = timeSheet;
        }
        // GET api/<controller>/GetAllTimeSheet
        [HttpPost]
        public IEnumerable<TimeSheet> GetAllTimeSheet([FromBody]DataRequest<List<object>, bool> values)
        {
            var dateRange = values.Value;
            bool isPaid = values.Value1;
            var data = _timeSheet.GetAllTimeSheet(dateRange[0], dateRange[1], isPaid);
            return data;
        }

        // GET api/<controller>/GetAllEmployeeTimeSheet
        [HttpPost]
        public IEnumerable<TimeSheet> GetAllEmployeeTimeSheet(List<object> dateRange)
        {
            var data = _timeSheet.GetAllEmployeeTimeSheet(dateRange[0], dateRange[1]);
            return data;
        }

        // GET api/<controller>/GetAllTimeSheet
        public IEnumerable<TimeSheetDetail> GetTimeSheetDetail(string id)
        {
            var data = _timeSheet.GetTimeSheetDetail(id);
            return data;
        }
        // POST api/<controller>/approvetimesheet
        [HttpPost]
        public void ApproveTimeSheet([FromBody]List<object> dateRange)
        {
            _timeSheet.approveTimeSheet(dateRange[0], dateRange[1], RequestContext.Principal.Identity.Name);
        }

        // POST api/<controller>/AddTimeSheetEmployee
        [HttpPost]
        public void AddTimeSheetEmployee([FromBody]TimeSheet timeSheet)
        {
            _timeSheet.addTimeSheetEmployee(timeSheet, RequestContext.Principal.Identity.Name);
        }
        // PUST api/<controller>/updateTimeSheet
        [HttpPut]
        public void updateTimeSheet([FromBody]DataRequest<string, List<TimeSheetDetail>> values)
        {
            string data = values.Value1.SerializeObject<TimeSheetDetail>();
            string date = values.Value;
            _timeSheet.updateTimeSheet(date, data, RequestContext.Principal.Identity.Name);
        }
        // PUST api/<controller>/updateLessPAYG
        [HttpPut]
        public void updateLessPAYG([FromBody]DataRequest<List<string>, List<TimeSheetLessPAYG>> values)
        {
            string data = values.Value1.SerializeObject<TimeSheetLessPAYG>();
            var dateRange = values.Value;
            _timeSheet.updateLessPAYG(dateRange[0], dateRange[1], data, RequestContext.Principal.Identity.Name);
        }
        // PUST api/<controller>/updateLessPAYGStatus/1
        [HttpPut]
        public void updateLessPAYGStatus(int id, [FromBody]DataRequest<string> values)
        {
            _timeSheet.updateLessPAYGStatus(id, values.Value, RequestContext.Principal.Identity.Name);
        }
    }
}
