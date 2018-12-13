using BWC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWC.Model;
using Dapper;

namespace BWC.Core.Repositories.MySql
{
    public class DiscountRepository : BaseRepository, IDiscount
    {
        public bool Delete(object id, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_DeleteDiscount (@id)
                    ", new { id });
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("get discount info: ", e);
                return false;
            }
        }

        public IEnumerable<Discount> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Discount> GetAll(int discountType)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Discount>(@"
                    call sp_GetAllDiscount (@discountType)
                    ", new { discountType }).ToList();
                }
            }
            catch (Exception e)
            {

                BWC.Core.Common.LogManager.LogError("get discount list: ", e);
                return null;
            }
        }

        public Discount GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Discount>(@"
                    call sp_GetDiscount (@id)
                    ", new { id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

                BWC.Core.Common.LogManager.LogError("get discount info: ", e);
                return null;
            }
        }

        public bool Insert(Discount entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_InsertDiscount (
                          @ObjectId
                        , @ProductId
                        , @DiscountValue
                        , @DiscountType
                    )
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {

                BWC.Core.Common.LogManager.LogError("Insert discount: ", e);
                return false;
            }
        }

        public bool Update(Discount entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateDiscount (
                          @Id
                        , @ObjectId
                        , @ProductId
                        , @DiscountValue
                        , @DiscountType
                    )
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {

                BWC.Core.Common.LogManager.LogError("Update discount: ", e);
                return false;
            }
        }
    }
}
