using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using BWC.Core.Interfaces;
using Dapper;

namespace BWC.Core.Repositories.MySql
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
                      call  sp_GetAllOrderProduct (@orderId)
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
                       call sp_GetOrderProduct (@id)
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
                    call sp_InsertOrderProduct (
                                    @OrderId,
                                    @OrderType,
                                    @ProductId,
                                    @MaterialId,
                                    @LocationId,
                                    @ColorId,
                                    @ColorName,
                                    @ControlSideId ,
                                    @UnitId ,
                                    @Drop ,
                                    @Width ,
                                    @Quantity ,
                                    @Discount,
                                    @ExtendPrice,
                                    @UnitPrice ,
                                    @TotalAmount,
                                    @DeliveryNo,
                                    @DeliveryDate,
                                    @Received ,
                                    @BackOrder,
                                    @Step,                                    
                                    @AMTExcGST,
                                    @GST,
                                    @AMTIncGST,
                                    @ReceivedAMTExcGST,
                                    @ReceivedGST,
                                    @ReceivedAMTIncGST,
                                    @MaterialColorName,
                                    @BoxColorName,
                                    @BarColorName,
                                    @GuideColorName,
                                    @ControlColorName,
                                    @StripeColorName,
                                    @RollId,
                                    @BKTId,
                                    @TSplineId,
                                    @BSplineId,
                                    @FlapId,
                                    @ControlHBOL,
                                    @TubeDia,
                                    @Notes
                                )
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
                    call sp_DeleteOrderProduct (@id)", new { id });
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
                    call sp_UpdateOrderProduct (
                                    @Id,
                                    @OrderId,
                                    @OrderType,
                                    @ProductId,
                                    @MaterialId,
                                    @LocationId,
                                    @LocationName,
                                    @ColorId,
                                    @ColorName,
                                    @ControlSideId ,
                                    @UnitId ,
                                    @Drop ,
                                    @Width ,
                                    @Quantity ,
                                    @Discount,
                                    @ExtendPrice,
                                    @UnitPrice ,
                                    @TotalAmount,
                                    @DeliveryNo,
                                    @DeliveryDate,
                                    @Received ,
                                    @BackOrder,
                                    @Step,                                    
                                    @AMTExcGST,
                                    @GST,
                                    @AMTIncGST,
                                    @ReceivedAMTExcGST,
                                    @ReceivedGST,
                                    @ReceivedAMTIncGST,

                                    @MaterialColorName,
                                    @BoxColorName,
                                    @BarColorName,
                                    @GuideColorName,
                                    @ControlColorName,
                                    @StripeColorName,
                                    @RollId,
                                    @BKTId,
                                    @TSplineId,
                                    @BSplineId,
                                    @FlapId,
                                    @ControlHBOL,
                                    @TubeDia,
                                    @Notes
                                )
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
                    call sp_InsertOrderProductFromOrder (
                                    @orderId,
                                    @values   
                                )
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
                    call sp_MarkToCompleteOrderProduct(@values)
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
                    call sp_UpdateOrderProductStep(@orderId,@step)
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
