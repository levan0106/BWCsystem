using BWC.Core.Interfaces;
using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BWC.Core.Repositories.MsSql
{
    public class SettingRepository : BaseRepository, ISetting
    {
        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Setting> GetAll()
        {
            throw new NotImplementedException();
        }

        public Setting GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Setting>(@"
                      call  sp_GetAllSettings
                    ", new { }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All Setting: ", e);
                throw;
            }
        }

        public bool Insert(Setting entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Setting entity, string userLogin)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Execute(@"
                    call sp_UpdateSettings (
                                    @Id,
                                    @GST,
                                    @Contribution
                            )
                    ", entity);
                    return true;
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Update Setting: ", e);
                return false;
            }
        }
    }
}
