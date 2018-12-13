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
    public class MaterialController : BaseApiController
    {
        readonly IMaterial _material;
        public MaterialController(IMaterial material)
        {
            _material = material;
        }
        // GET api/<controller>
        [JwtAuthenticationAttribute]
        public IEnumerable<Material> Get()
        {
            var data = _material.GetAll();
            return data;
        }

        // GET api/<controller>/5
        [JwtAuthenticationAttribute]
        public Material Get(int id)
        {
            var data = _material.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        [JwtAuthenticationAttribute]
        public void Post([FromBody]Material values)
        {
            _material.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/5
        [JwtAuthenticationAttribute]
        public void Put(int id, [FromBody]Material values)
        {
            _material.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        [JwtAuthenticationAttribute]
        public void Delete(int id)
        {
            _material.Delete(id, RequestContext.Principal.Identity.Name);
        }
    }
}