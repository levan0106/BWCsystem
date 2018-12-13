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
    public class ColorRepository:BaseRepository , IColor
    {
        public IEnumerable<Color> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Color>(@"
                  call  sp_GetAllColor
                ", new { }).ToList();
            }
        }

        public Color GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Color entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Color entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}
