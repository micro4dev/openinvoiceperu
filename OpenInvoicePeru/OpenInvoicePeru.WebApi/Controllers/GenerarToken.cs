using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Firmado;
using OpenInvoicePeru.RestService;
using OpenInvoicePeru.WebApi.Utils;
using OpenInvoicePeru.Xml;
using Swashbuckle.Swagger.Annotations;
using System.Web.Http;

namespace OpenInvoicePeru.WebApi.Controllers
{
    public class GenerarTokenController : ApiController
    {
        private readonly IValidezComprobanteHelper _helper;
        public GenerarTokenController(IValidezComprobanteHelper helper)
        {
            _helper = helper;
        }
        [HttpPost]
        [Route("api/GenerarToken")]
        [SwaggerResponse(200, "OK", typeof(TokenResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(RespuestaComun))]
        [SwaggerResponse(209, "Conflicts", typeof(RespuestaComun))]
        public IHttpActionResult GenerarToken(CrearTokenRequest request)
        {
            var response = new TokenResponse();

            var result = _helper.GenerarTokenSunat(request.ClientId,
                request.ClientSecret, request.UserName, request.Password);

            response.AccessToken = result.Result.AccessToken;
            response.Exito = result.Success;
            response.MensajeError = result.ErrorMessage;

            return Ok(response);

        }
    }
}