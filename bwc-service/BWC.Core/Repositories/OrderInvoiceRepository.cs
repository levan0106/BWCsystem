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
    public class OrderInvoiceRepository : BaseRepository, IOrderInvoice
    {
        public bool Delete(object id, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    exec sp_DeleteOrderInvoice @Id=@id", new { id });
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
                    sp_GetAllOrderInvoice @OrderId=@id
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
                    sp_GetOrderInvoice @Id=@id
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
                     sp_InsertOrderInvoice 
                                    @OrderId=@OrderId,
                                    @InvoiceNo=@InvoiceNo,
                                    @InvoiceAmount=@InvoiceAmount,
                                    @CutLengthCharge=@CutLengthCharge,
                                    @DeliveryCharge=@DeliveryCharge,
                                    @AMTExcGST=@AMTExcGST,
                                    @GST=@GST,
                                    @AMTIncGST=@AMTIncGST,
                                    @ActiveStatus=@ActiveStatus
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
                     sp_UpdateOrderInvoice 
                                    @Id=@Id,
                                    @OrderId=@OrderId,
                                    @InvoiceNo=@InvoiceNo,
                                    @InvoiceAmount=@InvoiceAmount,
                                    @CutLengthCharge=@CutLengthCharge,
                                    @DeliveryCharge=@DeliveryCharge,
                                    @AMTExcGST=@AMTExcGST,
                                    @GST=@GST,
                                    @AMTIncGST=@AMTIncGST,
                                    @ActiveStatus=@ActiveStatus
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
