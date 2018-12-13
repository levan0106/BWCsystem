using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Authentication
{
    public static class Auth
    {
        //public static HttpResponseMessage ResponAuthToken(this HttpRequestMessage Request, string token)
        //{
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //    response.Headers.Add("Token", token);
        //    response.Headers.Add("Access-Control-Expose-Headers", "Token");
        //    return response;
        //}
        public static bool CheckParameters(string username, string password)
        {
            // should check in the database
            return true;
        }
    }
}
