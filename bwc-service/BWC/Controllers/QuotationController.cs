using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BWC.Core.Common;
using BWC.Authentication.Filters;
using BWC.Common;
using BWC.Core.Interfaces;
using BWC.Model;

namespace BWC.Controllers
{
    [JwtAuthenticationAttribute]
    public class QuotationController : BaseApiController
    {
        readonly IOrder _quotation;
        readonly IOrderComponent _quotationComponent;
        readonly IOrderProduct _quotationProduct;
        readonly IOrderInvoice _quotationInvoice;
        readonly IOrderPayment _quotationPayment;
        readonly IOrderItem _quotationItem;
        public QuotationController(IOrder order, IOrderComponent orderComponent, IOrderProduct orderProduct, IOrderInvoice orderInvoice, IOrderPayment orderPayment, IOrderItem orderitem)
        {
            _quotation = order;
            _quotationComponent = orderComponent;
            _quotationProduct = orderProduct;
            _quotationInvoice = orderInvoice;
            _quotationPayment = orderPayment;
            _quotationItem = orderitem;
        }
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            var data = _quotation.GetAll((int)Enums.OrderType.Quotation);
            return data;
        }
        // GET api/<controller>
        [HttpPost]
        public IEnumerable<Order> GetAllByDateRange(List<DateTime> dateRange)
        {
            if (dateRange == null)
                return new List<Order>();

            var data = _quotation.GetAllByDateRange((int)Enums.OrderType.Quotation, dateRange[0], dateRange[1]);
            return data;
        }
    }
}
