using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BWC.Model;
using BWC.Core.Interfaces;
using BWC.Authentication.Filters;

namespace BWC.Controllers
{

    [TokenAuthenticationAttribute]
    public class DiscountController : BaseApiController
    {
        readonly IDiscount _discount;

        public DiscountController(IDiscount discount)
        {
            _discount = discount;
        }
        // GET: api/action/Discount/getall/1
        [HttpGet]
        public IEnumerable<Discount> All(int id)
        {
            IEnumerable<Discount> data = _discount.GetAll(id);
            return data;
        }

        // GET: api/Discount/5
        [HttpGet]
        public Discount Get(int id)
        {
            Discount data = _discount.GetInfo(id);
            return data;
        }

        // POST: api/Discount
        [HttpPost]
        public void Post([FromBody]Discount value)
        {
            _discount.Insert(value, RequestContext.Principal.Identity.Name);
        }

        // PUT: api/Discount/5
        [HttpPut]
        public void Put(int id, [FromBody]Discount value)
        {
            _discount.Update(value, RequestContext.Principal.Identity.Name);
        }

        // DELETE: api/Discount/5
        [HttpDelete]
        public void Delete(int id)
        {
            _discount.Delete(id, RequestContext.Principal.Identity.Name);
        }
    }
}
