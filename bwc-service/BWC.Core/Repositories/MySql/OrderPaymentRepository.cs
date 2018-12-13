using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Core.Interfaces;
using BWC.Model;
using Dapper;

namespace BWC.Core.Repositories.MySql
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
                    call sp_DeleteOrderPayment (@id)", new { id });
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
                    call sp_GetAllOrderPayment (@id)
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
                    call sp_GetOrderPayment (@id)
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
                    call sp_InsertOrderPayment (
                                    @OrderId,
                                    @AmountPaid,
                                    @PaymentType,
                                    @DatePaid,
                                    @ActiveStatus
                                )
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
                    call sp_UpdateOrderPayment (
                                    @Id,
                                    @OrderId,
                                    @PaymentType,
                                    @AmountPaid,
                                    @ActiveStatus,
                                    @DatePaid
                                )
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
