using BWC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using Dapper;

namespace BWC.Core.Repositories
{
    public class ProductPriceRepository:BaseRepository,IProductPrice
    {

        public IEnumerable<ProductPrice> GetAllByProductMaterial(int productMaterialId, int? priceType)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<ProductPrice>(@"
                        sp_GetProductMaterialPrice @ProductMaterialId=@productMaterialId, @PriceType=@priceType
                    ", new { productMaterialId, priceType }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("get all product material price: ", e);
                return new List<ProductPrice>();
            }
        }

        public IEnumerable<ProductPrice> GetAllByProduct(int productId, int? priceType)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<ProductPrice>(@"
                        sp_GetAllProductPrice @ProductId=@productId, @PriceType=@priceType
                    ", new { productId, priceType }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("get all product price: ", e);
                return new List<ProductPrice>();
            }
        }
        public IEnumerable<ProductPrice> GetListGroupPriceByProduct(int productMaterialId, int? priceType)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    return connection.Query<ProductPrice>(@"
                        sp_GetListGroupPriceByProduct @ProductMaterialId=@productMaterialId, @PriceType=@priceType
                    ", new { productMaterialId, priceType }).ToList();
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("get all product price: ", e);
                    return new List<ProductPrice>();
                }
            }
        }

        public IEnumerable<ProductPrice> AllPriceByGroup(object groupId, int? priceType)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    return connection.Query<ProductPrice>(@"
                        sp_GetAllProductPriceByGroup @GroupId=@groupId, @PriceType=@priceType
                    ", new { groupId, priceType }).ToList();
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("get all product price by group: ", e);
                    return new List<ProductPrice>();
                }
            }
        }

        public bool Update(int productPriceId, object groupId, string values, int? priceType, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {

                    connection.Execute(@"
                    EXEC sp_UpdateProductMaterialPrice @ProductMaterialId=@productPriceId,@GroupId=@groupId, @Xml=@values, @PriceType=@priceType
                    ", new { values, groupId, productPriceId, priceType });
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update product price: ", e);
                    return false;
                }
            }
        }
        public bool CopyPrice(int productPriceId, int? priceType, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    EXEC sp_CopyProductMaterialPrice @ProductMaterialId=@productPriceId, @PriceType=@priceType
                    ", new { productPriceId, priceType });
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Copy product price: ", e);
                    return false;
                }
            }
        }
        public IEnumerable<ProductPrice> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductPrice GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ProductPrice entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    EXEC sp_DeleteProductPrice @Id=@id
                    ", new {  id });
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete product price: ", e);
                    return false;
                }
            }
            
        }

        public bool Update(ProductPrice entity, string userLogin)
        {
            throw new NotImplementedException();
        }


    }
}
