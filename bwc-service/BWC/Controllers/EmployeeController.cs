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
    public class EmployeeController : BaseApiController
    {
        readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }
        // GET api/<controller>
        [JwtAuthenticationAttribute]
        public IEnumerable<Employee> Get()
        {
            var data = _employee.GetAll();
            return data;
        }

        // GET api/<controller>/5
        [JwtAuthenticationAttribute]
        public Employee Get(int id)
        {
            var data = _employee.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        [JwtAuthenticationAttribute]
        public void Post([FromBody]Employee values)
        {
            _employee.Insert(values, RequestContext.Principal.Identity.Name );
        }

        // PUT api/<controller>/5
        [JwtAuthenticationAttribute]
        public void Put(int id, [FromBody]Employee values)
        {
            _employee.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        [JwtAuthenticationAttribute]
        public void Delete(int id)
        {
            _employee.Delete(id,RequestContext.Principal.Identity.Name);
        }
    }
}