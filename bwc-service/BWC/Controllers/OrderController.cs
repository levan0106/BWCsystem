using BWC.Authentication.Filters;
using BWC.Common;
using BWC.Core.Common;
using BWC.Core.Export;
using BWC.Core.Interfaces;
using BWC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

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
        readonly IOrderService _orderService;
        public OrderController(IOrder order, IOrderComponent orderComponent,IOrderProduct orderProduct,
            IOrderInvoice orderInvoice, IOrderPayment orderPayment,IOrderItem orderitem, IMakerSheet makerSheet,
            IOrderService orderService)
        {
            _order = order;
            _orderComponent = orderComponent;
            _orderProduct = orderProduct;
            _orderInvoice = orderInvoice;
            _orderPayment = orderPayment;
            _orderItem = orderitem;
            _makerSheet = makerSheet;
            _orderService = orderService;
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

            return dictionary;
        }

        [HttpGet]
        public HttpResponseMessage ExportMakerSheet(Int64 id)
        {
            // Get datas
            var orderData = _order.GetInfo(id);
            var productData = _makerSheet.GetAllProducts(id);
            var productComponentData = _makerSheet.GetAllProductComponents(id);
            var componentData = _makerSheet.GetAllComponents(id);

            //update Product step to in-process: 2
            _orderProduct.UpdateProductStep(id, 2, RequestContext.Principal.Identity.Name);

            var productDistinct = productData.GroupBy(test => test.ProductName)
                   .Select(grp => grp.First())
                   .ToList();

            GeneratePdf pdf = new GeneratePdf();
            Dictionary<string, string> columns = new Dictionary<string, string>();
            Dictionary<string, string> detailColumns = new Dictionary<string, string>();
            string caution = string.Empty;
            string bodyContent = string.Empty;
            string generalInfo = @"<table style='font-size: 9pt;'>
                                    <tr><td>SALE ID:</td><td>{0}</td></tr>
                                    <tr><td>CUSTOMER:</td><td>{1}</td></tr>
                                    <tr><td>NAME/REF:</td><td>{2}</td></tr>
                                    <tr><td>SITE ADDRESS:</td><td>{3}</td></tr>
                                    <tr><td>SALE DATE:</td><td>{4}</td></tr>
                                    <tr><td>REQ DATE:</td><td>{5}</td></tr>
                                    </table>";
            bodyContent += string.Format(generalInfo, 
                orderData.Id, 
                orderData.CustomerName, 
                orderData.OrderRefNo, 
                orderData.CustomerAddress, 
                orderData.OrderDate, 
                orderData.PickupDateForOrderOnly);

            int totalPage = productDistinct.Count();
            for (int i = 0; i < totalPage; i++)
            {
                MakerSheet pro = productDistinct[i];
                switch (pro.CategoryCode)
                {
                    case "ROLLERBLIND":
                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlSideId", "CTRL"},
                            {"ControlHBOL", "CTRL HEIGHT"},
                            {"ControlColorName", "CTRL COL"},
                            {"BKTId", "BKT"},
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialColorName", "M. COL"},
                            {"MaterialWidth", "M WIDTH"},
                            {"MaterialDrop", "M DROP"},
                            {"Quantity", "QTY"},
                            {"TubeWidth", "TUBE WITH"},
                            {"BarColorName", "BAR COL"},
                            {"RollId", "ROLL"},
                            {"Notes", "NOTE"},
                        };
                        break;
                    case "ZIPSCREEN":

                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlSideId", "CTRL"},
                            {"Notes", "NOTE"},
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialWidth", "M WIDTH"},
                            {"MaterialDrop", "M DROP"},
                            {"Quantity", "QTY"},
                            {"TSplineName", "B. SPLINE"},
                            {"TubeDia", "TUBE DIA"},
                            {"TubeWidth", "TUBE WIDTH"},
                            {"BarColorName", "BAR COL"},
                            {"BAR WIDTH", "BAR WIDTH"},
                            {"BoxColorName", "BOX COL"},
                            {"BOX WIDTH", "BOX WIDTH"},
                            {"GuideColorName", "GUIDE COL"},
                            {"OuterTrackDrop", "OUTER GUIDE"},
                            {"InnerTrackDrop", "INNER GUIDE"},
                            //{"Notes", "NOTE"}
                        };
                        caution = "SIDE OF MATERIAL = NO ZIP (ON 200MM TOP AND 20MM BOTTOM)";
                        break;
                    case "FLYSCREEN":
                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlSideId", "SPRING"},
                            {"RollId", "TAB"},
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"FrameWidth", "FRAME WIDTH"},
                            {"FrameDrop", "FRAME DROP"},
                            {"Quantity", "QTY"},
                            {"Notes", "NOTE"},
                        };
                        break;
                    case "SECURITY":
                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlColorName", "CTRL"},
                            {"ControlHBOL", "BOL"},
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"FrameWidth", "FRAME WIDTH"},
                            {"FrameDrop", "FRAME DROP"},
                            {"Quantity", "QTY"},
                            {"MeshWidth", "MESH WIDTH"},
                            {"MeshDrop", "MESH DROP"}
                        };
                        break;
                    case "FLYDOOR":
                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlColorName", "CTRL"},
                            {"ControlHBOL", "BOL"},
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"FrameWidth", "FRAME WIDTH"},
                            {"FrameDrop", "FRAME DROP"},
                            {"Quantity", "QTY"},
                            {"MeshWidth", "MESH WIDTH"},
                            {"MeshDrop", "MESH DROP"},
                            {"Notes", "NOTE"},
                        };
                        break;
                    case "RS":
                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlColorName", "CTRL"},
                            {"Notes", "NOTE"},
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialColorName", "M. COL"},
                            {"Quantity", "QTY"},
                            {"BarColorName", "BAR COL"},
                            {"RollId", "ROLL"},
                            {"BoxTypeDrop", "Box Type"},
                            {"BoxLength", "Box Length"},
                            {"BottomBarHeight", "BottomBar Height"},
                            {"BottomBarLength", "BottomBar Length"},
                            {"SlatHeight", "Slat Height"},
                            {"SlatAmount", "Slat Amount"},
                            {"SlatLenght", "Slat Length"},
                            {"AxleLenght", "Axle Length"},
                        };
                        break;
                    default:
                        columns = new Dictionary<string, string>
                        {
                            {"LocationName", "LOC"},
                            {"Width", "WIDTH"},
                            {"Drop", "DROP"},
                            {"Quantity", "QTY"},
                            {"MaterialName", "MATERIAL"},
                            {"MaterialColorName", "COL"},
                            {"ControlColorName", "CTRL"}
                        };
                        detailColumns = new Dictionary<string, string>
                        {
                            {"MaterialColorName", "COL"},
                            {"MaterialWidth", "M WIDTH"},
                            {"MaterialDrop", "M DROP"},
                            {"Quantity", "QTY"},
                            {"TSpline", "T. SPLINE"},
                            {"BSpline", "B. SPLINE"},
                            {"Flap", "FLAP"},
                            {"TubeDia", "TUBE DIA"},
                            {"TubeWidth", "TUBE WIDTH"},
                            {"BarColorName", "BAR COL"},
                            {"BarWidth", "BAR WIDTH"},
                            {"BoxColorName", "BOX COL"},
                            {"BoxWidth", "BOX WIDTH"}
                        };
                        break;
                }

                // Title
                bodyContent += pdf.FormatTitle(string.Format("{0} ({1} of {2} in order)", pro.ProductName, i + 1, totalPage));
                var products = productData.Where(p => p.ProductName == pro.ProductName).ToList();
                bodyContent += pdf.ToDataTable<MakerSheet>(products, columns, "Id");

                // Title detail
                bodyContent += pdf.FormatTitle("MAKING DETAIL");
                bodyContent += pdf.FormatTitle(caution,"p");
                bodyContent += pdf.ToDataTable<MakerSheet>(products, detailColumns, "Id");

                // Product Components
                bodyContent += pdf.FormatTitle("Product Components");
                var productComponents = productComponentData.Where(p => p.ProductId == pro.ProductId)
                    .GroupBy(item => item.ComponentCode)
                    .Select(cl => new MakerSheetComponent()
                    {
                        ProductId = cl.First().ProductId,
                        ComponentName = cl.First().ComponentName,
                        ComponentCode = cl.First().ComponentCode,
                        Quantity = cl.Sum(s => s.Quantity),
                        ColorName = cl.First().ColorName,
                        Size = cl.First().Size
                    })
                    .ToList();
                columns = new Dictionary<string, string>
                {
                    {"ComponentName", "NAME"},
                    {"Quantity", "QTY"},
                    {"ColorName", "COLOUR"},
                    {"Size", "SIZE"}
                };
                bodyContent += pdf.ToDataTable<MakerSheetComponent>(productComponents, columns);

                if (pro.CategoryCode == "SECURITY")
                {
                    foreach (var product in products)
                    {

                        string image = @"<br/>
                                        {3}
                                        <table>
                                            <tr>
                                                <td width='100px' height='150px' style='border: 1px solid #ccc;text-align:center; vertical-align: middle;'>
                                                    Frame
                                                </td>
                                                <td style='transform: rotate(270deg);'>
                                                    <span>{0}</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style='text-align:center;'>
                                                    <span>{1}</span>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table> 
                                        <div>Note:{2}</div>
                                        ";
                        bodyContent += string.Format(image, product.Drop, product.Width, product.Notes, product.MaterialName);
                    }
                }
                // Break to new page
                if (i + 1 < totalPage)
                {
                    bodyContent += pdf.NewPage;
                }
            }

            // Components
            var components = componentData.GroupBy(item => new { item.ComponentCode, item.Size })
                    .Select(cl => new MakerSheetComponent()
                    {
                        ProductId = cl.First().ProductId,
                        ComponentName = cl.First().ComponentName,
                        ComponentCode = cl.First().ComponentCode,
                        Quantity = cl.Sum(s => s.Quantity),
                        ColorName = cl.First().ColorName,
                        Size = cl.First().Size
                    }).ToList();
            if (components.Count() > 0)
            {
                // Break to new page
                bodyContent += pdf.NewPage;
                bodyContent += pdf.FormatTitle("OTHER ITEMS");
                columns = new Dictionary<string, string>
                {
                    {"ComponentName", "NAME"},
                    {"Quantity", "QTY"},
                    {"ColorName", "COLOUR"},
                    {"Size", "SIZE"}
                };
                bodyContent += pdf.ToDataTable<MakerSheetComponent>(components, columns);
            }

            string html = pdf.HtmlBody(bodyContent);

            string fileName = string.Format("BWC_ProductSheet_{0}", id);
            byte[] stream= pdf.GenerateFromHtml(html, fileName);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream)
            };
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return response;
        }
        #endregion

        #region Service
        // GET api/<controller>/AllServices/5
        [HttpGet]
        public IEnumerable<OrderService> AllServices(Int64 id)
        {
            var data = _orderService.GetAll(id);
            return data;
        }

        // GET api/<controller>/Service/5
        [HttpGet]
        public OrderService Service(int id)
        {
            var data = _orderService.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Service
        [HttpPost]
        public void Service([FromBody]OrderService values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            _orderService.Insert(values, RequestContext.Principal.Identity.Name);
        }
        // PUT api/<controller>/Service/5
        [HttpPut]
        public void Service(Int64 id, [FromBody]OrderService values)
        {
            values.OrderType = (int)Enums.OrderType.Order;
            _orderService.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteService/5
        [HttpDelete]
        public void DeleteService(int id)
        {
            _orderService.Delete(id, RequestContext.Principal.Identity.Name);
        }

        #endregion
    }
}
