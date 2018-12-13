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
using BWC.Core.Common;
using System.Xml.Serialization;
using System.IO;
using BWC.Common;

namespace BWC.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    [JwtAuthenticationAttribute]
    public class ProductController : BaseApiController
    {
        readonly IProduct _product;
        readonly IProductComponent _productComponent;
        readonly IProductMaterial _productMaterial;
        readonly IProductPrice _productPrice;
        public ProductController(IProduct product, IProductComponent productComponent , IProductMaterial productMaterial, IProductPrice productPrice)
        {
            _product = product;
            _productComponent = productComponent;
            _productMaterial = productMaterial;
            _productPrice = productPrice;
        }
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            var data = _product.GetAll();
            return data;
        }

        // GET api/<controller>/5
        [HttpGet]
        public Product GetInfo(int id)
        {
            var data = _product.GetInfo(id);
            return data;
        }

        // POST api/<controller>
        [HttpPost]
        public void Insert([FromBody]Product values)
        {
            _product.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Update(int id, [FromBody]Product values)
        {
            _product.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _product.Delete(id, RequestContext.Principal.Identity.Name);
        }

        #region Component
        // GET api/<controller>/AllComponent/5
        [HttpGet]
        public IEnumerable<ProductComponent> AllComponent(int id)
        {
            var data = _productComponent.GetAll(id);
            return data;
        }

        // GET api/<controller>/Component/5
        [HttpGet]
        public ProductComponent Component(int id)
        {
            var data = _productComponent.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Component
        [HttpPost]
        public void Component([FromBody]ProductComponent values)
        {
            _productComponent.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Component/5
        [HttpPut]
        public void Component(int id, [FromBody]ProductComponent values)
        {
            _productComponent.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteComponent/5
        [HttpDelete]
        public void DeleteComponent(int id)
        {
            _productComponent.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Material
        // GET api/<controller>/AllMaterial/5
        [JwtAuthenticationAttribute]
        [HttpGet]
        public IEnumerable<ProductMaterial> AllMaterial(int id)
        {
            var data = _productMaterial.GetAll(id);
            return data;
        }

        // GET api/<controller>/Material/5
        [JwtAuthenticationAttribute]
        [HttpGet]
        public ProductMaterial Material(int id)
        {
            var data = _productMaterial.GetInfo(id);
            return data;
        }
        // POST api/<controller>/Material
        [JwtAuthenticationAttribute]
        [HttpPost]
        public void Material([FromBody]ProductMaterial values)
        {
            _productMaterial.Insert(values, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Material/5
        [JwtAuthenticationAttribute]
        [HttpPut]
        public void Material(int id, [FromBody]ProductMaterial values)
        {
            _productMaterial.Update(values, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeleteMaterial/5
        [JwtAuthenticationAttribute]
        [HttpDelete]
        public void DeleteMaterial(int id)
        {
            _productMaterial.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion

        #region Price


        // GET api/action/<controller>/AllPriceByProduct/5
        [HttpGet]
        public IEnumerable<ProductPrice> AllPriceByProduct(int id)
        {
            var data = _productPrice.GetAllByProduct(id, (int)Enums.PriceType.Sell);
            return data;
        }

        // GET api/action/<controller>/AllPurchasePriceByProduct/5
        [HttpGet]
        public IEnumerable<ProductPrice> AllPurchasePriceByProduct(int id)
        {
            var data = _productPrice.GetAllByProduct(id, (int)Enums.PriceType.Purchase);
            return data;
        }

        // GET api/action/<controller>/ListGroupPriceByProduct/5
        [HttpGet]
        public IEnumerable<ProductPrice> ListGroupPriceByProduct(int id)
        {
            var data = _productPrice.GetListGroupPriceByProduct(id, (int)Enums.PriceType.Sell);
            return data;
        }

        // GET api/action/<controller>/ListGroupPurchasePriceByProduct/5
        [HttpGet]
        public IEnumerable<ProductPrice> ListGroupPurchasePriceByProduct(int id)
        {
            var data = _productPrice.GetListGroupPriceByProduct(id, (int)Enums.PriceType.Purchase);
            return data;
        }

        // GET api/<controller>/AllPriceByProductMaterial/5
        [HttpGet]
        public IEnumerable<ProductPrice> AllPriceByProductMaterial(int id)
        {
            var data = _productPrice.GetAllByProductMaterial(id, (int)Enums.PriceType.Sell);
            return data;
        }

        // GET api/<controller>/AllPurchasePriceByProductMaterial/5
        [HttpGet]
        public IEnumerable<ProductPrice> AllPurchasePriceByProductMaterial(int id)
        {
            var data = _productPrice.GetAllByProductMaterial(id, (int)Enums.PriceType.Purchase);
            return data;
        }

        // GET api/action/<controller>/AllProductPriceByGroup/636655461680130000
        [HttpGet]
        public IEnumerable<ProductPrice> AllPriceByGroup(Int64 id)
        {
            var data = _productPrice.AllPriceByGroup(id,null);
            return data;
        }


        // GET api/action/<controller>/CopyPrice/5
        [HttpPost]
        public bool CopyPrice(int id)
        {
            var data = _productPrice.CopyPrice(id, (int)Enums.PriceType.Sell, RequestContext.Principal.Identity.Name);
            return data;
        }

        // GET api/action/<controller>/CopyPrice/5
        [HttpPost]
        public bool CopyPurchasePrice(int id)
        {
            var data = _productPrice.CopyPrice(id, (int)Enums.PriceType.Purchase, RequestContext.Principal.Identity.Name);
            return data;
        }

        // PUT api/<controller>/Price/5
        [HttpPut]
        public void Price(int id, [FromBody]DataRequest<Int64, List<ProductPrice>> values)
        {
            string data = values.Value1.SerializeObject<ProductPrice>();
            Int64 groupId = values.Value;
            _productPrice.Update(id, groupId, data, (int)Enums.PriceType.Sell, RequestContext.Principal.Identity.Name);
        }

        // PUT api/<controller>/Price/5
        [HttpPut]
        public void PurchasePrice(int id, [FromBody]DataRequest<Int64, List<ProductPrice>> values)
        {
            string data = values.Value1.SerializeObject<ProductPrice>();
            Int64 groupId = values.Value;
            _productPrice.Update(id,groupId, data, (int)Enums.PriceType.Purchase, RequestContext.Principal.Identity.Name);
        }

        // DELETE api/<controller>/DeletePrice/5
        [HttpDelete]
        public void DeletePrice(Int64 id)
        {
            _productPrice.Delete(id, RequestContext.Principal.Identity.Name);
        }
        #endregion
    }
}