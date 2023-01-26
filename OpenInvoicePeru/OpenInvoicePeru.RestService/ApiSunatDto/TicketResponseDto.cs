using Newtonsoft.Json;

namespace OpenInvoicePeru.RestService.ApiSunatDto
{
    public class TicketResponseDto
    {
        [JsonProperty("codRespuesta")]
        public string CodRespuesta { get; set; }

        [JsonProperty("arcCdr")]
        public string ArcCdr { get; set; }

        [JsonProperty("indCdrGenerado")]
        public string IndCdrGenerado { get; set; }
    }
}