using BWC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BWC.Model;

namespace BWC.Core.Repositories.MySql
{
    class UserRepository : BaseRepository, IUser
    {

        public User Authenticate(string userName, string password)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                   call sp_AuthenticateUser (
                                    @userName, 
                                    @password
                                )
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
                call sp_GetUserById (@id)
                ", new { id }).FirstOrDefault();
            }
        }

        public bool Insert(User User, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                call sp_InsertUser (
                                @UserName, 
                                @Password, 
                                @FullName,
                                @FirstName, 
                                @LastName, 
                                @Email,
                                @SecurityAnswer,
                                @SecurityQuestion,
                                @ActiveStatus,
                                @Role,
                                @Sex
                                )
                ", User);

                return connection.Query(@"
                    call sp_GetUserById(@UserName)
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
                    call sp_DeleteUser(@id)", new { id });
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
                    call sp_InsertUser (
                                    @UserName, 
                                    @Password, 
                                    @FullName,
                                    @FirstName, 
                                    @LastName, 
                                    @Email,
                                    @SecurityAnswer,
                                    @SecurityQuestion,
                                    @ActiveStatus,
                                    @Role, 
                                    @RecId,
                                    @Sex
                                )
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
                    call sp_GetUserByRole (@roleName)
                ", new { roleName });

            }
        }

        public IEnumerable<User> GetAllByPermission(string permissionName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                   call  sp_GetUserByPermission (@permissionName)"
                    , new { permissionName });

            }
        }

        public IEnumerable<User> GetAll(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                call sp_GetAllUsers (
                                @PageSize,
                                @CurrentPage,
                                @GetTotalRow,
                                @SortBy
                                )
                ", new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).ToList();
            }
        }

        public int GetAll_ToTal(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                call sp_GetAllUsers(
                                @PageSize,
                                @CurrentPage,
                                @GetTotalRow,
                                @SortBy
                                )
                ", new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).FirstOrDefault();
            }
        }

        public bool ValidateUserEmail(string userName, string email)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                call sp_ValidateUserEmail(@userName,@Email)
                ", new { userName, email }).Any();
            }
        }
    }
}
