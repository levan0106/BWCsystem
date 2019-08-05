using BWC.Core.Interfaces;
using BWC.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BWC.Core.Repositories.MySql
{
    public class UserTokenRepository: BaseRepository, IUserToken
    {
        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserToken> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserToken GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(UserToken entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                    call sp_InsertUserToken(@UserName, @AccessToken, @RefreshToken)
                ", entity);
                return true;
            }
        }

        public bool Update(UserToken entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                    call sp_UpdateUserToken(@UserName, @AccessToken, @RefreshToken)
                ", entity);
                return true;
            }
        }
        
        public bool ValiddateRefreshToken(UserToken entity)
        {
            using (var connection = GetConnection())
            {
                UserToken token = connection.Query<UserToken>(@"
                    call sp_GetUserToken(@UserName, @AccessToken, @RefreshToken)
                ", entity).FirstOrDefault();

                return token != null && !token.IsExpired(entity.ExpiresIn);
            }
        }

    }
}
