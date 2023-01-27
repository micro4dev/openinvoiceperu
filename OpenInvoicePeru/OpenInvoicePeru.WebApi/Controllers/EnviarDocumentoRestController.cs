using System.Linq;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Firmado;
using OpenInvoicePeru.Servicio;
using System.Threading.Tasks;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Security.Cryptography;
using System;
using RestSharp;
using OpenInvoicePeru.RestService.ApiSunatDto;
using Newtonsoft.Json;

namespace OpenInvoicePeru.WebApi.Controllers
{
    /// <inheritdoc />
    public class EnviarDocumentoRestController : ApiController
    {
        private readonly ISerializador _serializador;

        /// <inheritdoc />
        public EnviarDocumentoRestController(ISerializador serializador)
        {
            _serializador = serializador;
        }

        /// <summary>
        /// Envia el Documento a SUNAT/OSE
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, "OK", typeof(EnviarResumenResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        [SwaggerResponse(209, "Conflicts", typeof(string))]
        public async Task<IHttpActionResult> EnviarDocumentoRest([FromBody]EnviarDocumentoRequest request)
        {
            //var response = new EnviarDocumentoResponse();
            var response = new EnviarResumenResponse();

            try
            {
                var nombreArchivo = $"{request.Ruc}-{request.TipoDocumento}-{request.IdDocumento}";
                var tramaZip = await _serializador.GenerarZip(request.TramaXmlFirmado, nombreArchivo);

                string sha256;

                var bytesZip = Convert.FromBase64String(tramaZip);

                using (var sha = new SHA256CryptoServiceProvider())
                {
                    sha256 = string.Concat(sha.ComputeHash(bytesZip).Select(x => x.ToString("X2"))).ToLower();
                    Console.WriteLine(sha256);
                }

                string url = $"{request.EndPointUrl}/{nombreArchivo}";
                //await Task.Delay(455);

                var restClient = new RestClient(url);
                var restRequest = new RestRequest(Method.POST);

                restRequest.AddHeader("Authorization", "Bearer " + request.Token);
                restRequest.AddHeader("Content-Type", "application/json");

                GuiaRemisionRequest guiaRemisionRequest = new GuiaRemisionRequest
                {
                    Archivo =
                    {
                        NomArchivo = $"{nombreArchivo}.zip",
                        ArcGreZip = tramaZip,
                        HashZip = sha256
                    }
                };

                restRequest.AddParameter("application/json",
                    JsonConvert.SerializeObject(guiaRemisionRequest),
                    ParameterType.RequestBody);

                var result = await restClient.ExecuteAsync(restRequest);
                //await Task.Delay(455);

                response.Exito = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    response.Exito = true;
                    response.NroTicket = JsonConvert.DeserializeObject<TicketEnvioResponseDto>(result.Content).NumTicket;
                    //await Task.Delay(455);
                }
                else
                {
                    response.Exito = false;
                    response.MensajeError = result.Content;
                    //await Task.Delay(455);
                }

                

                //_servicioSunatDocumentos.Inicializar(new ParametrosConexion
                //{
                //    Ruc = request.Ruc,
                //    UserName = request.UsuarioSol,
                //    Password = request.ClaveSol,
                //    EndPointUrl = request.EndPointUrl
                //});

                //var resultado = _servicioSunatDocumentos.EnviarDocumento(new DocumentoSunat
                //{
                //    TramaXml = tramaZip,
                //    NombreArchivo = $"{nombreArchivo}.zip"
                //});

                //if (!resultado.Exito)
                //{
                //    response.Exito = false;
                //    response.MensajeError = resultado.MensajeError;
                //}
                //else
                //{
                //    response = await _serializador.GenerarDocumentoRespuesta(resultado.ConstanciaDeRecepcion);
                //    // Quitamos la R y la extensión devueltas por el Servicio.
                //    response.NombreArchivo = nombreArchivo;


                //}



                //return response;
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
