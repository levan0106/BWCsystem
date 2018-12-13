using BWC.Authentication.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BWC.Authentication;
using System.Web.Http.Cors;
using BWC.Core.Interfaces;
using BWC.Model;

namespace BWC.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthorizationController:BaseApiController
    {
        readonly IUser _user;
        public AuthorizationController(IUser user)
        {
            _user = user;
        }
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage login(string userName, string password)
        {
            if (CheckParameters(userName, password))
            {
                string token = JwtManager.GenerateToken(userName);
                return ResponAuthToken(token);
            }

            return AuthenFail();
        }
        private HttpResponseMessage ResponAuthToken(string token)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            return response;
        }
        private HttpResponseMessage AuthenFail()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            return response;
        }
        private bool CheckParameters(string username, string password)
        {
            // should check in the database
            User user = _user.Authenticate(username, password);
            return user.Authenticate == 1;
        }
    }
}