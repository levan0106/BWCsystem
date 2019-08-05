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
using BWC.Authentication.Models;
using BWC.Common;

namespace BWC.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticationController:BaseApiController
    {
        private int TokenExpiresIn
        {
            get
            {
                int expiresIn = 1800; // 30 mins
                int.TryParse(ConfigManager.GetAppSetting("TokenExpiresIn"), out expiresIn);
                return expiresIn;
            }
        }
        private int TokenExpiredInMax
        {
            get
            {
                int tokenExpiredInMax = 1800; // 30 mins
                int.TryParse(ConfigManager.GetAppSetting("TokenExpiredInMax"), out tokenExpiredInMax);
                return tokenExpiredInMax;
            }
        }

        readonly IUser _user;
        readonly IUserToken _userToken;

        public AuthenticationController(IUser user,IUserToken userToken)
        {
            _user = user;
            _userToken = userToken;
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] UserLoginModel user)
        {
            var responseJson = new TokenResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                StatusText = HttpStatusCode.Unauthorized.ToString()
            };

            if (CheckParameters(user.UserName, user.Password))
            {
                responseJson = DoGenerateToken(user.UserName);
            }

            return ResponAuthToken(responseJson);
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage RefreshToken([FromBody] RefreshTokenModel token)
        {
            var responseJson = new TokenResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                StatusText = HttpStatusCode.Unauthorized.ToString()
            };

            if (token != null && ValidateRefreshToken(null, token.AccessToken, token.RefreshToken))
            {
                string user = TokenManager.GetUserInfo(token.AccessToken);
                responseJson = DoGenerateToken(user);
            }

            return ResponAuthToken(responseJson);
        }

        private TokenResponse DoGenerateToken(string user)
        {
            string newRefreshToken = TokenManager.GenerateRefreshToken();// generate new refresh token
            string newAccessToken = TokenManager.GenerateToken(user, TokenExpiresIn);
            TokenResponse tokenResponse = new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = TokenExpiresIn,
                StatusCode = (int)HttpStatusCode.OK,
                StatusText = HttpStatusCode.OK.ToString()
            };

            _userToken.Insert(new UserToken()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                UserName = user
            }, user);

            return tokenResponse;
        }

        private HttpResponseMessage ResponAuthToken(TokenResponse tokenResponse)
        {
            HttpResponseMessage response = Request.CreateResponse((HttpStatusCode)tokenResponse.StatusCode, tokenResponse.StatusText);
            response.Headers.Add("access_token", tokenResponse.AccessToken);
            response.Headers.Add("refresh_token", tokenResponse.RefreshToken);
            response.Headers.Add("expires_in", tokenResponse.ExpiresIn.ToString());
            response.Headers.Add("Access-Control-Expose-Headers", "access_token,refresh_token,expires_in");
            return response;
        }

        private bool CheckParameters(string username, string password)
        {
            // should check in the database
            User user = _user.Authenticate(username, password);
            return user.Authenticate == 1;
        }

        private bool ValidateRefreshToken(string username, string accessToken, string refreshToken)
        {
            return ValidateRefreshToken(new UserToken()
            {
                UserName = username,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = TokenExpiredInMax // The token don't expired over 5 mins
            });
        }

        private bool ValidateRefreshToken(UserToken userToken)
        {
            return _userToken.ValiddateRefreshToken(userToken);
        }
    }
}