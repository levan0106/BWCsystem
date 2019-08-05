using BWC.Authentication.Filters;
using BWC.Common;
using BWC.Core.Interfaces;
using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BWC.Core.Common;
using System.Globalization;

namespace BWC.Controllers
{
    
    [TokenAuthenticationAttribute]
    public class PurchaseController : BaseApiController
    {
        readonly IOrder _purchase;
        readonly IOrderComponent _purchaseComponent;
        readonly IOrderProduct _purchaseProduct;
        readonly IOrderInvoice _purchaseInvoice;
        readonly IOrderPayment _purchasePayment;
        readonly IOrderItem _purchaseItem;
        public PurchaseController(IOrder purchase, IOrderComponent purchaseComponent,IOrderProduct purchaseProduct,IOrderInvoice purchaseInvoice, IOrderPayment purchasePayment,IOrderItem purchaseitem)
        {
            _purchase = purchase;
            _purchaseComponent = purchaseComponent;
            _purchaseProduct = purchaseProduct;
            _purchaseInvoice = purchaseInvoice;
            _purchasePayment = purchasePayment;
            _purchaseItem = purchaseitem;
        }
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            var data = _purchase.GetAll((int)Enums.OrderType.Purchase);
            return data;
        }
        // GET api/<controller>
       [HttpPost]
        public IEnumerable<Order> GetAllByDateRange(List<object> dateRange)
        {
            if (dateRange == null || dateRange.Count == 0)
                return new List<Order>();
            //DateTime from = DateTime.ParseExact(dateRange[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime to = DateTime.ParseExact(dateRange[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var data = _purchase.GetAllByDateRange((int)Enums.OrderType.Purchase, dateRange[0], dateRange[1]);
            return data;
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<OrderItem> GetAllItems()
        {
            var data = _purchaseItem.GetAll((int)Enums.OrderType.Purchase);
            return data;
        }

        // GET api/<controller>/5
        [HttpGet]
        public Order GetInfo(Int64 id)
        {
            var data = _purchase.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        [HttpPost]
        public void Insert([FromBody]Order values)
        {
            values.OrderType = (int)Enums.OrderType.Purchase;
            _purchase.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Update(Int64 id, [FromBody]Order values)
        {
            values.OrderType = (int)Enums.OrderType.Purchase;
            values.Id = id;
            _purchase.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(Int64 id)
        {
            _purchase.Delete(id, RequestContext.Principal.Identity.Name);
        }

        // POST api/<controller>
        [HttpPut]
        public bool CopyToNew(Int64 id, [FromBody]DataRequest<Int64> values)
        {
            bool result = _purchase.Copy(id,values.Value, RequestContext.Principal.Identity.Name);
            return result;
        }

        #region Product
        // GET api/<controller>/AllProduct/5
        [HttpGet]
        public IEnumerable<OrderProduct> AllProduct(Int64 id)
        {
            var data = _purchaseProduct.GetAll(id);
            return data;
        }

        // GET api/<controller>/Product/5
        [HttpGet]
        public OrderProduct Product(int id)
        {
            var data = _purchaseProduct.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Product
        [HttpPost]
        public void Product([FromBody]OrderProduct values)
        {
            values.OrderType = (int)Enums.OrderType.Purchase;
            values.Step = 1;//New
            _purchaseProduct.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Product/5
        [HttpPut]
        public void Product(int id, [FromBody]OrderProduct values)
        {
            values.OrderType = (int)Enums.OrderType.Purchase;
            _purchaseProduct.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteProduct/5
        [HttpDelete]
        public void DeleteProduct(int id)
        {
            _purchaseProduct.Delete(id, RequestContext.Principal.Identity.Name);
        }
        [HttpPut]
        public void AddProductFromOrder(Int64 id, [FromBody]List<OrderProduct> values)
        {
            string data = values.SerializeObject<OrderProduct>();
            _purchaseProduct.AddFromOrder(id, data, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Component
        // GET api/<controller>/AllComponent/5
        [HttpGet]
        public IEnumerable<OrderComponent> AllComponent(Int64 id)
        {
            var data = _purchaseComponent.GetAll(id);
            return data;
        }

        // GET api/<controller>/Component/5
        [HttpGet]
        public OrderComponent Component(int id)
        {
            var data = _purchaseComponent.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Component
        [HttpPost]
        public void Component([FromBody]OrderComponent values)
        {
            values.OrderType = (int)Enums.OrderType.Purchase;
            values.Step = 1;//New
            _purchaseComponent.Insert(values, RequestContext.Principal.Identity.Name);
        }
        // PUT api/<controller>/Component/5
        [HttpPut]
        public void Component(Int64 id, [FromBody]OrderComponent values)
        {
            values.OrderType = (int)Enums.OrderType.Purchase;
            _purchaseComponent.Update(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Component/5
        [HttpPut]
        public void ListComponent(Int64 id, [FromBody]List<Component> values)
        {
            string data = values.SerializeObject<Component>();
            _purchaseComponent.Update(id, data, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteComponent/5
        [HttpDelete]
        public void DeleteComponent(int id)
        {
            _purchaseComponent.Delete(id, RequestContext.Principal.Identity.Name);
        }

        [HttpPut]
        public void AddComponentFromOrder(Int64 id, [FromBody]List<OrderComponent> values)
        {
            string data = values.SerializeObject<OrderComponent>();
            _purchaseComponent.AddFromOrder(id, data, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Invoice
        // GET api/<controller>/AllInvoice/5
        [HttpGet]
        public IEnumerable<OrderInvoice> AllInvoice(Int64 id)
        {
            var data = _purchaseInvoice.GetAll(id);
            return data;
        }

        // GET api/<controller>/Component/5
        [HttpGet]
        public OrderInvoice Invoice(int id)
        {
            var data = _purchaseInvoice.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Invoice
        [HttpPost]
        public void Invoice([FromBody]OrderInvoice values)
        {
            _purchaseInvoice.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Invoice/5
        [HttpPut]
        public void Invoice(int id, [FromBody]OrderInvoice values)
        {
            _purchaseInvoice.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteInvoice/5
        [HttpDelete]
        public void DeleteInvoice(int id)
        {
            _purchaseInvoice.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Payment
        // GET api/<controller>/AllPayment/5
        [HttpGet]
        public IEnumerable<OrderPayment> AllPayment(Int64 id)
        {
            var data = _purchasePayment.GetAll(id);
            return data;
        }

        // GET api/<controller>/Payment/5
        [HttpGet]
        public OrderPayment Payment(int id)
        {
            var data = _purchasePayment.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Payment
        [HttpPost]
        public void Payment([FromBody]OrderPayment values)
        {
            _purchasePayment.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Payment/5
        [HttpPut]
        public void Payment(int id, [FromBody]OrderPayment values)
        {
            _purchasePayment.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeletePayment/5
        [HttpDelete]
        public void DeletePayment(int id)
        {
            _purchasePayment.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion
    }
}
