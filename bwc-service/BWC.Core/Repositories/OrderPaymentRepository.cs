using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Core.Interfaces;
using BWC.Model;
using Dapper;

namespace BWC.Core.Repositories
{
    public class OrderPaymentRepository : BaseRepository, IOrderPayment
    {
        public bool Delete(object id, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    exec sp_DeleteOrderPayment @Id=@id", new { id });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("delete Payment in purchase", e);
                return false;
            }
        }

        public IEnumerable<OrderPayment> GetAll()
        {
            return null;
        }

        public IEnumerable<OrderPayment> GetAll(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<OrderPayment>(@"
                    sp_GetAllOrderPayment @OrderId=@id
                ", new { id }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get all Payment by purchase", e);
                return null;
            }
        }

        public OrderPayment GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<OrderPayment>(@"
                    sp_GetOrderPayment @Id=@id
                    ", new { id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get all Payment by purchase", e);
                return null;
            }
        }

        public bool Insert(OrderPayment entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                     sp_InsertOrderPayment 
                                    @OrderId=@OrderId,
                                    @PaymentType=@PaymentType,
                                    @AmountPaid=@AmountPaid,
                                    @ActiveStatus=@ActiveStatus,
                                    @DatePaid=@DatePaid
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("insert Payment in purchase", e);
                return false;
            }
        }

        public bool Update(OrderPayment entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                     sp_UpdateOrderPayment 
                                    @Id=@Id,
                                    @OrderId=@OrderId,
                                    @PaymentType=@PaymentType,
                                    @AmountPaid=@AmountPaid,
                                    @ActiveStatus=@ActiveStatus,
                                    @DatePaid=@DatePaid
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("update Payment in purchase", e);
                return false;
            }
        }
    }
}
