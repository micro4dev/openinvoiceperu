using Newtonsoft.Json;

namespace OpenInvoicePeru.RestService.ApiSunatDto
{
    public class TicketEnvioResponseDto
    {
        [JsonProperty(PropertyName = "numTicket")]
        public string NumTicket { get; set; }
        [JsonProperty(PropertyName = "fecRecepcion")]
        public string FecRecepcion { get; set; }


    }
}