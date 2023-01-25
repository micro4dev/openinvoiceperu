using System.ComponentModel.DataAnnotations;

namespace OpenInvoicePeru.Comun.Dto.Intercambio
{
    public class CrearTokenRequest
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}