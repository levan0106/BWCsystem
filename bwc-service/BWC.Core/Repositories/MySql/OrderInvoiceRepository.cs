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
    public class OrderInvoiceRepository : BaseRepository, IOrderInvoice
    {
        public bool Delete(object id, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_DeleteOrderInvoice (@id)", new { id });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("delete invoice in purchase", e);
                return false;
            }
        }

        public IEnumerable<OrderInvoice> GetAll()
        {
            return null;
        }

        public IEnumerable<OrderInvoice> GetAll(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<OrderInvoice>(@"
                    call sp_GetAllOrderInvoice(@id)
                ", new { id }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get all invoice by purchase", e);
                return null;
            }
        }

        public OrderInvoice GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<OrderInvoice>(@"
                    call sp_GetOrderInvoice (@id)
                    ", new { id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get all invoice by purchase", e);
                return null;
            }
        }

        public bool Insert(OrderInvoice entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_InsertOrderInvoice (
                                    @OrderId,
                                    @InvoiceNo,
                                    @InvoiceAmount,
                                    @CutLengthCharge,
                                    @DeliveryCharge,
                                    @AMTExcGST,
                                    @GST,
                                    @AMTIncGST,
                                    @ActiveStatus
                                )
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert invoice in purchase", e);
                return false;
            }
        }

        public bool Update(OrderInvoice entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateOrderInvoice (
                                    @Id,
                                    @OrderId,
                                    @InvoiceNo,
                                    @InvoiceAmount,
                                    @CutLengthCharge,
                                    @DeliveryCharge,
                                    @AMTExcGST,
                                    @GST,
                                    @AMTIncGST,
                                    @ActiveStatus
                                )
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update invoice in purchase", e);
                return false;
            }
        }
    }
}
