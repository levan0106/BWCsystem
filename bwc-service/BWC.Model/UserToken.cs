using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class UserToken : Base
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }

        public bool IsExpired()
        {
            return IsExpired(ExpiresIn);
        }
        public bool IsExpired(int expiresIn)
        {
            return DateTime.Now.AddSeconds(-1 * expiresIn) > UpdateDTS;
        }
    }
}
