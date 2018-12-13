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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [JwtAuthenticationAttribute]
    public class CustomerController : BaseApiController
    {
        readonly ICustomer _customer;
        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }
        // GET api/<controller>
        public IEnumerable<Customer> Get()
        {
            var data = _customer.GetAll();
            return data;
        }

        // GET api/<controller>/5
        public Customer Get(int id)
        {
            var data = _customer.GetInfo(id);
            return data;
        }

        // GET api/<controller>/SystemInfo
        //[AllowAnonymous]
        //[HttpGet]
        //public Customer SystemInfo()
        //{
        //    var data = _customer.GetSystemInfo();
        //    return data;
        //}

        // POST api/<controller>
        public void Post([FromBody]Customer values)
        {
            _customer.Insert(values, RequestContext.Principal.Identity.Name );
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Customer values)
        {
            _customer.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            _customer.Delete(id,RequestContext.Principal.Identity.Name);
        }
    }
}