using BWC.Core.Interfaces;
using BWC.Model;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace BWC.Core.Repositories
{
    public class UserRepository: BaseRepository, IUser
    {
        public User Authenticate(string userName, string password)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                    CALL sp_AuthenticateUser(@userName, @Password)
                ", new { userName, password }).FirstOrDefault();
            }
        }
        public IEnumerable<User> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"SELECT * FROM SEC_User").ToList();
            }
        }
        public IEnumerable<User> GetAllByParent(string userName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"SELECT * FROM SEC_User WHERE ParentId=@userName", new { userName }).ToList();
            }
        }

        public User GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                sp_GetUserById @UserName=@id
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(User User, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                sp_InsertUser @UserName=@UserName, @Password=@Password, @FullName=@FullName ,@FirstName=@FirstName, @LastName=@LastName, @Email=@Email
                                    ,@SecurityAnswer=@SecurityAnswer,@SecurityQuestion=@SecurityQuestion,@ActiveStatus=@ActiveStatus,@Role=@Role,@Sex=@Sex
                ", User);

                return connection.Query(@"
                sp_GetUserById @UserName=@UserName
                ", new { User.UserName }).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    exec sp_DeleteUser @UserName=@id", new { id });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Update(User User, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_InsertUser @UserName=@UserName, @Password=@Password, @FullName=@FullName ,@FirstName=@FirstName, @LastName=@LastName, @Email=@Email
                                    ,@SecurityAnswer=@SecurityAnswer,@SecurityQuestion=@SecurityQuestion,@ActiveStatus=@ActiveStatus,@Role=@Role
                                    , @UpdateBy=@RecId,@Sex=@Sex
                    ", User);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }


        public IEnumerable<User> GetAllByRole(string roleName)
        {
            using (var connection = GetConnection())
            {

                return connection.Query<User>(@"
                     sp_GetUserByRole @Role=@roleName
                ", new { roleName });

            }
        }

        public IEnumerable<User> GetAllByPermission(string permissionName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                     sp_GetUserByPermission @Permission=@permissionName"
                    , new { permissionName });

            }
        }

        public IEnumerable<User> GetAll(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                EXEC sp_GetAllUsers @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ", new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).ToList();
            }
        }

        public int GetAll_ToTal(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                EXEC sp_GetAllUsers @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ", new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).FirstOrDefault();
            }
        }

        public bool ValidateUserEmail(string userName, string email)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_ValidateUserEmail @UserName=@userName,@Email=@Email
                ", new { userName, email }).Any();
            }
        }
    }
}
