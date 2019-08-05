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
    public class ColorController : BaseApiController
    {
        readonly IColor _color;
        public ColorController(IColor color)
        {
            _color = color;
        }
        // GET api/<controller>
        [AllowAnonymous]
        public IEnumerable<Color> Get()
        {
            var data = _color.GetAll();
            return data;
        }

        // GET api/<controller>/5
        [AllowAnonymous]
        public Color Get(int id)
        {
            var data = _color.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        [TokenAuthenticationAttribute]
        public void Post([FromBody]Color values)
        {
            _color.Insert(values, RequestContext.Principal.Identity.Name );
        }

        // PUT api/<controller>/5
        [TokenAuthenticationAttribute]
        public void Put(int id, [FromBody]Color values)
        {
            _color.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        [TokenAuthenticationAttribute]
        public void Delete(int id)
        {
            _color.Delete(id,RequestContext.Principal.Identity.Name);
        }
    }
}