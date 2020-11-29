﻿using Newtonsoft.Json;

namespace OpenInvoicePeru.DtoStandard.Modelos
{
    public class DocumentoRelacionado
    {
        [JsonProperty(Order = 1, Required = Required.Always)]
        public string NroDocumento { get; set; }

        [JsonProperty(Order = 2, Required = Required.Always)]
        public string TipoDocumento { get; set; }
    }
}