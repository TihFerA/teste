using System.Web.Http;
using System.Web.Http.Cors;

namespace Staff.Framework.Web.Helpers
{
    public class DefaultApiConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            /*
             * Habilita o CORS Globalmente 
             * Indica ao serviço para aceitar request Ajax de outras origens
             * (http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api)
             * Habilitar o CORS não significa ter suporte ao preflight. 
             * Esse suporte precisa ser feito com a implementação de um handler para o método OPTIONS
             */

            // TODO: Configurar origem a partir do arquivo de configuração 

            string methods = "GET,PUT,POST,PATCH,DELETE";
            var cors = new EnableCorsAttribute("*", "*", methods);
            cors.SupportsCredentials = true;
            config.EnableCors(cors);
            config.MessageHandlers.Add(new PreflightHandler("*", "*", methods));

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new StaffJSONResolver();
            //config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StaffJSONConverter());

        }
    }
}
