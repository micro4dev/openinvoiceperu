using System;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Firmado;
using OpenInvoicePeru.RestService.ApiSunatDto;
using OpenInvoicePeru.Servicio;
using RestSharp;
using Swashbuckle.Swagger.Annotations;

namespace OpenInvoicePeru.WebApi.Controllers
{
    /// <inheritdoc />
    public class ConsultarTicketRestController : ApiController
    {
        private readonly IServicioSunatDocumentos _servicioSunatDocumentos;
        private readonly ISerializador _serializador;

        /// <inheritdoc />
        public ConsultarTicketRestController(IServicioSunatDocumentos servicioSunatDocumentos, ISerializador serializador)
        {
            _servicioSunatDocumentos = servicioSunatDocumentos;
            _serializador = serializador;
        }

        /// <summary>
        /// Consulta el Ticket existen en SUNAT (Solo Produccion)
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, "OK", typeof(EnviarDocumentoResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        [SwaggerResponse(209, "Conflicts", typeof(string))]
        public async Task<IHttpActionResult> Post([FromBody] ConsultaTicketRequest request)
        {
            var response = new EnviarDocumentoResponse();

            try
            {
                string url = $"{request.EndPointUrl}/envios/{request.NroTicket}";
                //await Task.Delay(455);
                var restClient = new RestClient(url);
                var restRequest = new RestRequest(Method.GET);

                restRequest.AddHeader("Authorization", "Bearer " + request.Token);

                var result = await restClient.ExecuteAsync(restRequest);
                //await Task.Delay(455);

                response.Exito = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    TicketResponseDto ticketResponseDto =
                        JsonConvert.DeserializeObject<TicketResponseDto>(result.Content);

                    //await Task.Delay(455);


                    if (ticketResponseDto.CodRespuesta.Equals("0"))
                    {
                        response.Exito = true;
                        response.CodigoRespuesta = ticketResponseDto.CodRespuesta;
                        response.TramaZipCdr = ticketResponseDto.ArcCdr;
                        //await Task.Delay(455);

                    }
                    else
                    {
                        response.Exito = true;
                        response.CodigoRespuesta = ticketResponseDto.CodRespuesta;
                        response.TramaZipCdr = ticketResponseDto.ArcCdr;
                        response.MensajeError = result.Content;
                        //await Task.Delay(455);

                    }
                }
                else
                {
                    response.MensajeError = result.Content;
                    response.Exito = false;
                    //await Task.Delay(455);
                }

                //restRequest.AddHeader("Content-Type", "application/json");

                //_servicioSunatDocumentos.Inicializar(new ParametrosConexion
                //{
                //    Ruc = request.Ruc,
                //    UserName = request.UsuarioSol,
                //    Password = request.ClaveSol,
                //    EndPointUrl = request.EndPointUrl
                //});

                //var resultado = _servicioSunatDocumentos.ConsultarTicket(request.NroTicket);

                //if (!resultado.Exito)
                //{
                //    response.Exito = false;
                //    response.MensajeError = resultado.MensajeError;
                //}
                //else
                //    response = await _serializador.GenerarDocumentoRespuesta(resultado.ConstanciaDeRecepcion);
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
