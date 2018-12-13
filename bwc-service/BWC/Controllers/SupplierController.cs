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
    public class SupplierController : BaseApiController
    {
        readonly ISupplier _supplier;
        public SupplierController(ISupplier supplier)
        {
            _supplier = supplier;
        }
        // GET api/<controller>
        public IEnumerable<Supplier> Get()
        {
            var data = _supplier.GetAll();
            return data;
        }

        // GET api/<controller>/5
        public Supplier Get(int id)
        {
            var data = _supplier.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        public void Post([FromBody]Supplier values)
        {
            _supplier.Insert(values, RequestContext.Principal.Identity.Name );
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Supplier values)
        {
            _supplier.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            _supplier.Delete(id,RequestContext.Principal.Identity.Name);
        }
    }
}