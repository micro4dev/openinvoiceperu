using Newtonsoft.Json;

namespace OpenInvoicePeru.Comun.Dto.Intercambio
{
    public class GuiaRemisionRequest
    {
        [JsonProperty("archivo")]
        public Archivo Archivo { get; set; }

        public GuiaRemisionRequest()
        {
            Archivo = new Archivo();
        }

    }
}