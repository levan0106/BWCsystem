using BWC.Core.Interfaces;
using BWC.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BWC.Core.Repositories
{
    public class OrderServiceRepository:BaseRepository,IOrderService
    {
        public IEnumerable<OrderService> GetAll(object orderId)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<OrderService>(@"
                  sp_GetAllOrderService @OrderId=@orderId
                ", new { orderId }).ToList();
            }
        }

        public IEnumerable<OrderService> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderService GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<OrderService>(@"
                   sp_GetOrderService @Id=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(OrderService entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    sp_InsertOrderService 
                                    @OrderType=@OrderType,
                                    @OrderId=@OrderId,
                                    @Task=@Task,
                                    @Quantity=@Quantity,
                                    @Charge=@Charge,
                                    @ServiceDate=@ServiceDate,
                                    @ServiceTime=@ServiceTime
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Insert Service into Order: ", e);
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
                    sp_DeleteOrderService @Id=@id", new { id });
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Delete a Service of Order: ", e);
                    return false;
                }
            }
        }

        public bool Update(OrderService entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    sp_UpdateOrderService 
                                    @Id=@Id,                                 
                                    @OrderType=@OrderType,
                                    @OrderId=@OrderId,   
                                    @Task=@Task,
                                    @Quantity=@Quantity,
                                    @Charge=@Charge,
                                    @ServiceDate=@ServiceDate,
                                    @ServiceTime=@ServiceTime
                                )
                    ", entity);
                    return true;
                }
                catch(Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Service info in Order: ", e);
                    return false;
                }
            }
        }
    }
}
