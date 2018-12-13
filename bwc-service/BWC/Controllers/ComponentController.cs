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
    public class ComponentController : BaseApiController
    {
        readonly IComponent _component;
        public ComponentController(IComponent component)
        {
            _component = component;
        }
        // GET api/<controller>
        public IEnumerable<Component> Get()
        {
            var data = _component.GetAll();
            return data;
        }

        // GET api/<controller>/5
        [HttpGet]
        public Component Get(int id)
        {
            var data = _component.GetInfo(id);
            return data;
        }

        // GET api/action/component/GetAllBySupplier/1
        [HttpGet]
        public IEnumerable<Component> GetAllBySupplier(int id)
        {
            var data = _component.GetAllBySupplier(id);
            return data;
        }


        // POST api/<controller>
        public void Post([FromBody]Component values)
        {
            _component.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Component values)
        {
            _component.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            _component.Delete(id, RequestContext.Principal.Identity.Name);
        }
    }
}