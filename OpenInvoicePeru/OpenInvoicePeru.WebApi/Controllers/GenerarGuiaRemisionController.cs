using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using OpenInvoicePeru.Firmado;
using OpenInvoicePeru.RestService;
using OpenInvoicePeru.WebApi.Utils;
using OpenInvoicePeru.Xml;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenInvoicePeru.WebApi.Controllers
{
    /// <inheritdoc />
    public class GenerarGuiaRemisionController : ApiController
    {
        private readonly IValidezComprobanteHelper _helper;

        private readonly IDocumentoXml _documentoXml;
        private readonly ISerializador _serializador;

        /// <inheritdoc />
        public GenerarGuiaRemisionController(ISerializador serializador, IValidezComprobanteHelper helper)
        {
            _serializador = serializador;
            _documentoXml = new GuiaRemisionXml();
            _helper = helper; 
        }


        [HttpPost]
        [Route("api/GenerarGuiaRemision/GenerarToken")]
        [SwaggerResponse(200, "OK", typeof(TokenResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(RespuestaComun))]
        [SwaggerResponse(209, "Conflicts", typeof(RespuestaComun))]
        public IHttpActionResult GenerarTokenGre(CrearTokenRequest request)
        {
            var response = new TokenResponse();

            var result = _helper.GenerarTokenGre(request.ClientId,
                request.ClientSecret, request.UserName, request.Password);

            response.AccessToken = result.Result.AccessToken;
            response.Exito = result.Success;
            response.MensajeError = result.ErrorMessage;

            return Ok(response);

        }

        /// <summary>
        /// Genera el XML para la Guia de Remision.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, "OK", typeof(DocumentoResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        [SwaggerResponse(209, "Conflicts", typeof(string))]
        public async Task<IHttpActionResult> Post([FromBody] GuiaRemision documento)
        {
            var response = new DocumentoResponse();
            try
            {
                var notaCredito = _documentoXml.Generar(documento);
                response.TramaXmlSinFirma = await _serializador.GenerarXml(notaCredito);
                response.Exito = true;
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Pila = ex.StackTrace;
                response.Exito = false;
            }

            return Ok(response);
        }
    }
}