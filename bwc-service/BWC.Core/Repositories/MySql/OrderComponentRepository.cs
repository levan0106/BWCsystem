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
    public class OrderComponentRepository:BaseRepository,IOrderComponent
    {
        public IEnumerable<OrderComponent> GetAll(object orderId)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<OrderComponent>(@"
                  call sp_GetAllOrderComponent (@orderId)
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
                   call sp_GetOrderComponent (@id)
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
                    call sp_InsertOrderComponent (
                                    @OrderType,
                                    @OrderId,
                                    @ComponentId,
                                    @ColorId,
                                    @Quantity,
                                    @Price,
                                    @ExtendPrice,
                                    @ExtCharge,
                                    @UnitId,
                                    @UnitName,
                                    @Step,    
                                    @TotalAmount,
                                    @DeliveryNo,
                                    @DeliveryDate,
                                    @Received ,
                                    @BackOrder,                                
                                    @AMTExcGST,
                                    @GST,
                                    @AMTIncGST,
                                    @ReceivedAMTExcGST,
                                    @ReceivedGST,
                                    @ReceivedAMTIncGST,
                                    @Size,
                                    @Discount
                                )
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
                    call sp_DeleteOrderComponent (@id)", new { id });
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
                    call sp_UpdateOrderListComponent (
                                    @orderId,
                                    @values   
                                )
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
                    call sp_UpdateOrderComponent (
                                    @Id,                                 
                                    @OrderType,
                                    @OrderId,   
                                    @ComponentId,
                                    @ColorId,
                                    @Quantity,
                                    @Price,
                                    @ExtendPrice,
                                    @ExtCharge,
                                    @UnitId,
                                    @UnitName,
                                    @Step,   
                                    @TotalAmount,
                                    @DeliveryNo,
                                    @DeliveryDate,
                                    @Received ,
                                    @BackOrder,                                 
                                    @AMTExcGST,
                                    @GST,
                                    @AMTIncGST,
                                    @ReceivedAMTExcGST,
                                    @ReceivedGST,
                                    @ReceivedAMTIncGST,
                                    @Size,
                                    @Discount
                                )
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
                    call sp_InsertOrderComponentFromOrder (
                                    @orderId,
                                    @values   
                                )
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
                    call sp_MarkToCompleteOrderComponent (@values)
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
