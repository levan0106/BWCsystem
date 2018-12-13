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
    public class CategoryController : BaseApiController
    {
        readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        // GET api/<controller>
        public IEnumerable<Category> Get()
        {
            var data = _category.GetAll();
            return data;
        }

        // GET api/<controller>/5
        [HttpGet]
        public Category Get(int id)
        {
            var data = _category.GetInfo(id);
            return data;
        }
        

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Category values)
        {
            _category.Update(values, RequestContext.Principal.Identity.Name);
        }
    }
}