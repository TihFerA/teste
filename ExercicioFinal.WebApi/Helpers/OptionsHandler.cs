using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Staff.Framework.Web.Helpers
{
    /// <summary>
    /// Classe para fazer a resposta a um metodo OPTIONS
    /// https://www.html5rocks.com/en/tutorials/cors/
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/httpclient-message-handlers
    /// </summary>
    public class PreflightHandler : DelegatingHandler
    {
        private string origins;
        private string headers;
        private string methods;

        /// <summary>
        /// Sincroniza com os mesmos atributos passado para o CORS
        /// </summary>
        /// <param name="cors"></param>
        public PreflightHandler(string origins, string headers, string methods)
        {
            this.origins = origins;
            this.headers = headers;
            this.methods = methods;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options) {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };

                if (request.Headers.Contains("Origin"))
                {
                    if (this.origins.Equals("*") || this.origins.Split(',').Any(x => x.Equals(request.Headers.GetValues("Origin"))))
                    {
                        response.Headers.Add("Access-Control-Allow-Origin", request.Headers.GetValues("Origin"));
                    }

                    // se tiver esse cabecalho indica um preflight
                    if (request.Headers.Contains("Access-Control-Request-Method"))
                    {
                        if (this.methods.Equals("*"))
                        {
                            response.Headers.Add("Access-Control-Allow-Methods", this.methods);
                        }
                        else
                        {
                            string requested = string.Join(",", request.Headers.GetValues("Access-Control-Request-Method"));
                            if (requested.Split(',').Intersect(this.methods.Split(',')).Count() > 0)
                            {
                                response.Headers.Add("Access-Control-Allow-Methods", this.methods);
                            }
                        }

                        if (request.Headers.Contains("Access-Control-Request-Headers"))
                        {
                            string requested = string.Join(",", request.Headers.GetValues("Access-Control-Request-Headers"));
                            if (this.headers.Equals("*"))
                            {
                                response.Headers.Add("Access-Control-Allow-Headers",
                                    requested);
                            }
                            else
                            {
                                response.Headers.Add("Access-Control-Allow-Headers",
                                    requested.Split(',').Intersect(this.headers.Split(',')));
                            }
                        }

                    }

                    response.Headers.Add("Access-Control-Allow-Credentials", "true");

                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}