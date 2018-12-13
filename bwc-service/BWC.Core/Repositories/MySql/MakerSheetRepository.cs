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
    public class MakerSheetRepository : BaseRepository, IMakerSheet
    {
        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MakerSheet> GetAll(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<MakerSheet>(@"
                      call sp_GetAllMakerSheet(@id)
                    ", new { id}).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All MakerSheet: ", e);
                throw;
            }
        }

        public IEnumerable<MakerSheet> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MakerSheetComponent> GetAllComponents(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<MakerSheetComponent>(@"
                      call sp_GetAllMakerSheetComponent(@id,0)
                    ", new { id }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All MakerSheet Component: ", e);
                throw;
            }
        }

        public IEnumerable<MakerSheetComponent> GetAllProductComponents(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<MakerSheetComponent>(@"
                      call sp_GetAllMakerSheetComponent(@id,1)
                    ", new { id }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All MakerSheet Product Component: ", e);
                throw;
            }
        }

        public IEnumerable<MakerSheet> GetAllProducts(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<MakerSheet>(@"
                      call sp_GetAllMakerSheet(@id)
                    ", new { id }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All MakerSheet: ", e);
                throw;
            }
        }

        public MakerSheet GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(MakerSheet entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(MakerSheet entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}
