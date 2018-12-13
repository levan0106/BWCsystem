using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Core.Interfaces
{
    public interface IUser : IRepository<User>
    {
        User Authenticate(string userName, string password);
        IEnumerable<User> GetAllByRole(string roleName);
        IEnumerable<User> GetAllByPermission(string permissionName);
        IEnumerable<User> GetAll(Paging paging);
        int GetAll_ToTal(Paging paging);
        bool ValidateUserEmail(string userName, string email);
    }
}
