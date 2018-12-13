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
    public class OrderComponentRepository:BaseRepository,IOrderComponent
    {
        public IEnumerable<OrderComponent> GetAll(object orderId)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<OrderComponent>(@"
                    sp_GetAllOrderComponent @OrderId=@orderId
                ", new { orderId }).ToList();
            }
        }

        public IEnumerable<OrderComponent> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderComponent GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<OrderComponent>(@"
                    sp_GetOrderComponent @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(OrderComponent entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_InsertOrderComponent 
                                    @OrderId=@OrderId,
                                    @OrderType=@OrderType,
                                    @ComponentId=@ComponentId,
                                    @ColorId=@ColorId,
                                    @Quantity=@Quantity,
                                    @Price=@Price,
                                    @ExtCharge=@ExtCharge,
                                    @UnitId=@UnitId,
                                    @UnitName=@UnitName,
                                    @Step=@Step,    
                                    @TotalAmount = @TotalAmount,
                                    @DeliveryNo = @DeliveryNo,
                                    @DeliveryDate = @DeliveryDate,
                                    @Received = @Received ,
                                    @BackOrder = @BackOrder,                                
                                    @AMTExcGST=@AMTExcGST,
                                    @GST=@GST,
                                    @AMTIncGST=@AMTIncGST,
                                    @ReceivedAMTExcGST=@ReceivedAMTExcGST,
                                    @ReceivedGST=@ReceivedGST,
                                    @ReceivedAMTIncGST=@ReceivedAMTIncGST,
                                    @ExtendPrice=@ExtendPrice,
                                    @Size=@Size,
                                    @Discount=@Discount
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert component into Order: ", e);
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
                    exec sp_DeleteOrderComponent @Id=@id", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete a component of Order: ", e);
                    return false;
                }
            }
        }

        public bool Update(Int64 orderId, string values, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateOrderListComponent 
                                    @OrderId=@orderId,
                                    @Xml=@values   
                    ", new { values, orderId });
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update component info in Order: ", e);
                    return false;
                }
            }
        }

        public bool Update(OrderComponent entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_UpdateOrderComponent 
                                    @Id=@Id,
                                    @OrderId=@OrderId,                                    
                                    @OrderType=@OrderType,
                                    @ComponentId=@ComponentId,
                                    @ColorId=@ColorId,
                                    @Quantity=@Quantity,
                                    @Price=@Price,
                                    @ExtCharge=@ExtCharge,
                                    @UnitId=@UnitId,
                                    @UnitName=@UnitName,
                                    @Step=@Step,   
                                    @TotalAmount = @TotalAmount,
                                    @DeliveryNo = @DeliveryNo,
                                    @DeliveryDate = @DeliveryDate,
                                    @Received = @Received ,
                                    @BackOrder = @BackOrder,                                 
                                    @AMTExcGST=@AMTExcGST,
                                    @GST=@GST,
                                    @AMTIncGST=@AMTIncGST,
                                    @ReceivedAMTExcGST=@ReceivedAMTExcGST,
                                    @ReceivedGST=@ReceivedGST,
                                    @ReceivedAMTIncGST=@ReceivedAMTIncGST,
                                    @ExtendPrice=@ExtendPrice,
                                    @Size=@Size,
                                    @Discount=@Discount
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update component info in Order: ", e);
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
                     sp_InsertOrderComponentFromOrder 
                                    @OrderId=@orderId,
                                    @Xml=@values   
                    ", new { values, orderId});
                        return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert components from order into order: ", e);
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
                     sp_MarkToCompleteOrderComponent
                                    @Xml=@values   
                    ", new { values });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Mark components to complete: ", e);
                return false;
            }
        }
    }
}
