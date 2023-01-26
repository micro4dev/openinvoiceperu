using Newtonsoft.Json;

namespace OpenInvoicePeru.Comun.Dto.Intercambio
{
    public class Archivo
    {
        [JsonProperty("nomArchivo")]
        public string NomArchivo { get; set; }

        [JsonProperty("arcGreZip")]
        public string ArcGreZip { get; set; }

        [JsonProperty("hashZip")]
        public string HashZip { get; set; }
    }
}