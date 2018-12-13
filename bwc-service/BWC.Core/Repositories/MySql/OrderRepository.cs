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
    public class OrderRepository:BaseRepository,IOrder
    {
        public IEnumerable<Order> GetAll()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Order>(@"
                   call sp_GetAllOrder
                ", new { }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get GetAll: ", e);
                return new List<Order>();
            }
            
        }
        public IEnumerable<Order> GetAllByDateRange(int orderType, object from, object to)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Order>(@"
                    call sp_GetAllOrderByDateRange (
                                    @orderType, 
                                    @from, 
                                    @to
                                )
                ", new { orderType, from, to }).ToList();
                }

            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get Order By Date Range: ", e);
                return new List<Order>();
            }
        }
        public IEnumerable<Order> GetAll(int orderType)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Order>(@"
                  call  sp_GetAllOrder (@orderType)
                ", new { orderType }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All Order: ", e);
                return new List<Order>();
            }
            
        }

        public Order GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Order>(@"
                call sp_GetOrder(@id)
                ", new { id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get Order Info: ", e);
                return new Order();
            }
            
        }

        public bool Insert(Order entity, string userLogin)
        {
                try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_InsertOrder  (
                                        @Id,
                                        @EmployeeId,
                                        @EmployeeName,
                                        @Step,
                                        @Taxes,
                                        @Surcharge,
                                        @Discount,
                                        @OrderDate,
                                        @FirtReceiveDate,
                                        @LastUpdate,
                                        @DeliveryDate,
                                        @DeliveryNo,
                                        @Notes,
                                        @SupplierId,
                                        @SupplierName,
                                        @SupplierAddress,
                                        @SupplierEmail,
                                        @SupplierPhone,
                                        @CustomerId,
                                        @CustomerName,
                                        @CustomerAddress,
                                        @CustomerEmail,
                                        @CustomerPhone,
                                        @OrderRefNo,
                                        @OrderType,
                                        @AMTExcGST,
                                        @GST,
                                        @AMTIncGST,
                                        @ActiveStatus,
                                        @InvoiceNoForOrderOnly,
                                        @InvoiceDateForOrderOnly,
                                        @PickupDateForOrderOnly,
                                        @CompleteDateForOrderOnly
                                    )
                    ", entity);

                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Insert Order: ", e);
                return false;
            }
        }

        public bool Delete(object id, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {

                    connection.Execute(@"
                    call sp_DeleteOrder (@id)", new { id });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Delete Order: ", e);
                return false;
            }
        }

        public bool Update(Order entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateOrder (
                                        @Id,
                                        @EmployeeId,
                                        @EmployeeName,
                                        @Step,
                                        @Taxes,
                                        @Surcharge,
                                        @Discount,
                                        @OrderDate,
                                        @FirtReceiveDate,
                                        @LastUpdate,
                                        @DeliveryDate,
                                        @DeliveryNo,
                                        @Notes,
                                        @SupplierId,
                                        @SupplierName,
                                        @SupplierAddress,
                                        @SupplierEmail,
                                        @SupplierPhone,
                                        @CustomerId,
                                        @CustomerName,
                                        @CustomerAddress,
                                        @CustomerEmail,
                                        @CustomerPhone,
                                        @OrderRefNo,
                                        @OrderType,
                                        @AMTExcGST,
                                        @GST,
                                        @AMTIncGST,
                                        @ActiveStatus,
                                        @InvoiceNoForOrderOnly,
                                        @InvoiceDateForOrderOnly,
                                        @PickupDateForOrderOnly,
                                        @CompleteDateForOrderOnly
                                    )
                    ", entity);

                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update Order: ", e);
                return false;
            }
        }

        public bool Copy(object id, object newId, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_CopyOrder(  
                                    @Id,
                                    @newId
                                )                                        
                    ", new {id,newId });

                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Copy Order: ", e);
                return false;
            }
        }
    }
}
