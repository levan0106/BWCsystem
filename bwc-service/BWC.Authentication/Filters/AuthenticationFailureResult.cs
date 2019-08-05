using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BWC.Authentication.Filters
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase { get; set; }
        public HttpRequestMessage Request { get; set; }
        public TokenStatus Status { get; set; }

        public AuthenticationFailureResult(HttpRequestMessage request, string reasonPhrase)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }
        public AuthenticationFailureResult(HttpRequestMessage request, string reasonPhrase, TokenStatus status)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
            Status = status;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };

            if(Status == TokenStatus.Expired)
            {
                response.Headers.Add("token_expired", "1");
                response.Headers.Add("Access-Control-Expose-Headers", "token_expired");
            }
            return response;
        }
    }
}
