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

namespace BWC.Controllers
{
    
    [TokenAuthenticationAttribute]
    public class OrderController : BaseApiController
    {
        readonly IOrder _order;
        readonly IOrderComponent _orderComponent;
        readonly IOrderProduct _orderProduct;
        readonly IOrderInvoice _orderInvoice;
        readonly IOrderPayment _orderPayment;
        readonly IOrderItem _orderItem;
        readonly IMakerSheet _makerSheet;
        public OrderController(IOrder order, IOrderComponent orderComponent,IOrderProduct orderProduct
            ,IOrderInvoice orderInvoice, IOrderPayment orderPayment,IOrderItem orderitem, IMakerSheet makerSheet)
        {
            _order = order;
            _orderComponent = orderComponent;
            _orderProduct = orderProduct;
            _orderInvoice = orderInvoice;
            _orderPayment = orderPayment;
            _orderItem = orderitem;
            _makerSheet = makerSheet;
        }
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            var data = _order.GetAll((int)Enums.OrderType.Order);
            return data;
        }
        // GET api/<controller>
       [HttpPost]
        public IEnumerable<Order> GetAllByDateRange(List<DateTime> dateRange)
        {
            if (dateRange == null)
                return new List<Order>();

            var data = _order.GetAllByDateRange((int)Enums.OrderType.Order, dateRange[0], dateRange[1]);
            return data;
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<OrderItem> GetAllItems()
        {
            var data = _orderItem.GetAll((int)Enums.OrderType.Order);
            return data;
        }

        // GET api/<controller>/5
        [HttpGet]
        public Order GetInfo(Int64 id)
        {
            var data = _order.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        [HttpPost]
        public void Insert([FromBody]Order values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            _order.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Update(Int64 id, [FromBody]Order values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            values.Id = id;
            _order.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(Int64 id)
        {
            _order.Delete(id, RequestContext.Principal.Identity.Name);
        }

        // POST api/<controller>
        [HttpPut]
        public bool CopyToNew(Int64 id, [FromBody]DataRequest<Int64> values)
        {
            bool result = _order.Copy(id,values.Value, RequestContext.Principal.Identity.Name);
            return result;
        }

        #region Product
        // GET api/<controller>/AllProduct/5
        [HttpGet]
        public IEnumerable<OrderProduct> AllProduct(Int64 id)
        {
            var data = _orderProduct.GetAll(id);
            return data;
        }

        // GET api/<controller>/Product/5
        [HttpGet]
        public OrderProduct Product(int id)
        {
            var data = _orderProduct.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Product
        [HttpPost]
        public void Product([FromBody]OrderProduct values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            values.Step = 1;//New
            _orderProduct.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Product/5
        [HttpPut]
        public void Product(int id, [FromBody]OrderProduct values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            _orderProduct.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteProduct/5
        [HttpDelete]
        public void DeleteProduct(int id)
        {
            _orderProduct.Delete(id, RequestContext.Principal.Identity.Name);
        }

        // POST api/<controller>/MarkToComplete
        [HttpPost]
        public void MarkToCompleteProduct([FromBody]List<OrderProduct> values)
        {
            string data = values.SerializeObject<OrderProduct>();
            _orderProduct.MarkToComplete(data, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Component
        // GET api/<controller>/AllComponent/5
        [HttpGet]
        public IEnumerable<OrderComponent> AllComponent(Int64 id)
        {
            var data = _orderComponent.GetAll(id);
            return data;
        }

        // GET api/<controller>/Component/5
        [HttpGet]
        public OrderComponent Component(int id)
        {
            var data = _orderComponent.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Component
        [HttpPost]
        public void Component([FromBody]OrderComponent values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            values.Step = 1;//New
            _orderComponent.Insert(values, RequestContext.Principal.Identity.Name);
        }
        // PUT api/<controller>/Component/5
        [HttpPut]
        public void Component(Int64 id, [FromBody]OrderComponent values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            _orderComponent.Update(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Component/5
        [HttpPut]
        public void ListComponent(Int64 id, [FromBody]List<Component> values)
        {
            string data = values.SerializeObject<Component>();
            _orderComponent.Update(id, data, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteComponent/5
        [HttpDelete]
        public void DeleteComponent(int id)
        {
            _orderComponent.Delete(id, RequestContext.Principal.Identity.Name);
        }

        // POST api/<controller>/MarkToComplete
        [HttpPost]
        public void MarkToCompleteComponent([FromBody]List<OrderComponent> values)
        {
            string data = values.SerializeObject<OrderComponent>();
            _orderComponent.MarkToComplete(data, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Invoice
        // GET api/<controller>/AllInvoice/5
        [HttpGet]
        public IEnumerable<OrderInvoice> AllInvoice(Int64 id)
        {
            var data = _orderInvoice.GetAll(id);
            return data;
        }

        // GET api/<controller>/Component/5
        [HttpGet]
        public OrderInvoice Invoice(int id)
        {
            var data = _orderInvoice.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Invoice
        [HttpPost]
        public void Invoice([FromBody]OrderInvoice values)
        {
            _orderInvoice.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Invoice/5
        [HttpPut]
        public void Invoice(int id, [FromBody]OrderInvoice values)
        {
            _orderInvoice.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteInvoice/5
        [HttpDelete]
        public void DeleteInvoice(int id)
        {
            _orderInvoice.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Payment
        // GET api/<controller>/AllPayment/5
        [HttpGet]
        public IEnumerable<OrderPayment> AllPayment(Int64 id)
        {
            var data = _orderPayment.GetAll(id);
            return data;
        }

        // GET api/<controller>/Payment/5
        [HttpGet]
        public OrderPayment Payment(int id)
        {
            var data = _orderPayment.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Payment
        [HttpPost]
        public void Payment([FromBody]OrderPayment values)
        {
            _orderPayment.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Payment/5
        [HttpPut]
        public void Payment(int id, [FromBody]OrderPayment values)
        {
            _orderPayment.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeletePayment/5
        [HttpDelete]
        public void DeletePayment(int id)
        {
            _orderPayment.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion
        #region MakerSheet
        // GET api/<controller>/MakerSheet/5
        [HttpGet]
        public Dictionary<string,IEnumerable<object>> MakerSheet(Int64 id)
        {
            var product = _makerSheet.GetAllProducts(id);
            var productComponent = _makerSheet.GetAllProductComponents(id);
            var component = _makerSheet.GetAllComponents(id);
            var dictionary = new Dictionary<string, IEnumerable<object>>();
            dictionary.Add("p", product);
            dictionary.Add("pc", productComponent);
            dictionary.Add("c", component);

            //update Product step to in-process: 2
            _orderProduct.UpdateProductStep(id, 2, RequestContext.Principal.Identity.Name);

            return dictionary;
        }
        #endregion
    }
}
