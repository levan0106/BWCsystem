using BWC.Authentication.Filters;
using BWC.Core.Interfaces;
using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BWC.Controllers
{
    [JwtAuthenticationAttribute]
    public class SettingController : BaseApiController
    {
        readonly ISetting _setting;
        public SettingController(ISetting setting)
        {
            _setting = setting;
        }
        // GET api/<controller>
        public Setting Get()
        {
            var data = _setting.GetInfo(1);
            return data;
        }

        // POST api/<controller>/5
        public void Post([FromBody]Setting values)
        {
            _setting.Update(values, RequestContext.Principal.Identity.Name);
        }
    }
}