using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using BWC.Core.Interfaces;
using Dapper;

namespace BWC.Core.Repositories
{
    public class OrderProductRepository:BaseRepository,IOrderProduct
    {
        public IEnumerable<OrderProduct> GetAll(object orderId)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    return connection.Query<OrderProduct>(@"
                        sp_GetAllOrderProduct @OrderId=@orderId
                    ", new { orderId }).ToList();
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Get all Product into Order: ", e);
                    return new List<OrderProduct>();
                }
            }
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderProduct GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    return connection.Query<OrderProduct>(@"
                        sp_GetOrderProduct @Id=@id
                    ", new { id }).FirstOrDefault();

                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Get all Product into Order: ", e);
                    return new OrderProduct();
                }
            }
        }

        public bool Insert(OrderProduct entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_InsertOrderProduct 
                                    @OrderId =@OrderId,
                                    @OrderType=@OrderType,
                                    @ProductId = @ProductId,
                                    @MaterialId = @MaterialId,
                                    @LocationId = @LocationId,
                                    @ColorId = @ColorId,
                                    @ColorName = @ColorName,
                                    @ControlSideId = @ControlSideId ,
                                    @UnitId = @UnitId ,
                                    @Drop = @Drop ,
                                    @Width = @Width ,
                                    @Quantity = @Quantity ,
                                    @Discount = @Discount,
                                    @ExtendPrice = @ExtendPrice,
                                    @UnitPrice = @UnitPrice ,
                                    @TotalAmount = @TotalAmount,
                                    @DeliveryNo = @DeliveryNo,
                                    @DeliveryDate = @DeliveryDate,
                                    @Received = @Received ,
                                    @BackOrder = @BackOrder,
                                    @Step=@Step,                                    
                                    @AMTExcGST=@AMTExcGST,
                                    @GST=@GST,
                                    @AMTIncGST=@AMTIncGST,
                                    @ReceivedAMTExcGST=@ReceivedAMTExcGST,
                                    @ReceivedGST=@ReceivedGST,
                                    @ReceivedAMTIncGST=@ReceivedAMTIncGST
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert Product into Order: ", e);
                    return false;
                }
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    exec sp_DeleteOrderProduct @Id=@id", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete a Product of Order: ", e);
                    return false;
                }
            }
        }

        public bool Update(OrderProduct entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateOrderProduct 
                                    @Id=@Id,
                                    @OrderId =@OrderId,
                                    @OrderType=@OrderType,
                                    @ProductId = @ProductId,
                                    @MaterialId = @MaterialId,
                                    @LocationId = @LocationId,
                                    @LocationName = @LocationName,
                                    @ColorId = @ColorId,
                                    @ColorName = @ColorName,
                                    @ControlSideId = @ControlSideId ,
                                    @UnitId = @UnitId ,
                                    @Drop = @Drop ,
                                    @Width = @Width ,
                                    @Quantity = @Quantity ,
                                    @Discount = @Discount,
                                    @ExtendPrice = @ExtendPrice,
                                    @UnitPrice = @UnitPrice ,
                                    @TotalAmount = @TotalAmount,
                                    @DeliveryNo = @DeliveryNo,
                                    @DeliveryDate = @DeliveryDate,
                                    @Received = @Received ,
                                    @BackOrder = @BackOrder,
                                    @Step=@Step,
                                    @AMTExcGST=@AMTExcGST,
                                    @GST=@GST,
                                    @AMTIncGST=@AMTIncGST,
                                    @ReceivedAMTExcGST=@ReceivedAMTExcGST,
                                    @ReceivedGST=@ReceivedGST,
                                    @ReceivedAMTIncGST=@ReceivedAMTIncGST
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Product info in Order: ", e);
                    return false;
                }
            }
        }
        public bool AddFromOrder(object orderId, string values, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                     sp_InsertOrderProductFromOrder 
                                    @OrderId=@orderId,
                                    @Xml=@values   
                    ", new { values, orderId });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert products from order into order: ", e);
                return false;
            }
        }

        public bool MarkToComplete(string values, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                     sp_MarkToCompleteOrderProduct
                                    @Xml=@values   
                    ", new { values });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Mark products to complete: ", e);
                return false;
            }
        }
        public bool UpdateProductStep(object orderId, int step, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateOrderProductStep(@i_OrderId=@orderId,@step=@step)
                    ", new { orderId, step });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update step of products: ", e);
                return false;
            }
        }
        
    }
}
