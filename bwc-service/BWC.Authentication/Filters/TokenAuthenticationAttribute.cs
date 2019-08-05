using BWC.Authentication.Jwt;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace BWC.Authentication.Filters
{
    public class TokenAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult(request, "Missing Jwt Token");
                return;
            }

            string token = authorization.Parameter;
            IPrincipal principal = await AuthenticateJwtToken(token, out TokenStatus status);

            if (status == TokenStatus.Expired)
            {
                context.ErrorResult = new AuthenticationFailureResult(request, "Token is expired.", status);
            }
            else if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult(request, "Invalid Jwt token");
            }
            else
            {
                context.Principal = principal;
            }
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token, out TokenStatus status)
        {
            status = ValidateToken(token, out string username);

            if (status == TokenStatus.Passed)
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        private static TokenStatus ValidateToken(string token, out string username)
        {
            username = null;
            TokenStatus tokenStatus = TokenStatus.Failed;

            ClaimsPrincipal simplePrinciple = TokenManager.GetPrincipal(token, ref tokenStatus);

            if (tokenStatus == TokenStatus.Expired || tokenStatus == TokenStatus.Failed) return tokenStatus;

            var identity = simplePrinciple != null ? simplePrinciple.Identity as ClaimsIdentity : null;
            if (identity == null)
                return TokenStatus.Failed;

            if (!identity.IsAuthenticated)
                return TokenStatus.Failed;

            username = identity.FindFirst(ClaimTypes.Name).Value;
            if (string.IsNullOrEmpty(username))
                return TokenStatus.Failed;

            // More validate to check whether username exists in system

            return TokenStatus.Passed;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            context.ChallengeWith("Bearer");
        }
    }
}
