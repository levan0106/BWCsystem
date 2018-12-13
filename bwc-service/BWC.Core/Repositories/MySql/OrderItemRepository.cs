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
    public class OrderItemRepository : BaseRepository, IOrderItem
    {
        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAll(int orderType)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<OrderItem>(@"
                    call sp_GetItemsInOrder (@orderType)
                ", new { orderType }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("get all items in Order: ", e);
                return null;
            }
        }

        public OrderItem GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(OrderItem entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(OrderItem entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}
