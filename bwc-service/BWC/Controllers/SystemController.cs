using BWC.Core.Interfaces;
using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BWC.Controllers
{
    public class SystemController : BaseApiController
    {
        readonly ICustomer _customer;
        public SystemController(ICustomer customer)
        {
            _customer = customer;
        }
        [AllowAnonymous]
        [HttpGet]
        public Customer SystemInfo()
        {
            var data = _customer.GetSystemInfo();
            return data;
        }
    }
}
