﻿using OpenInvoicePeru.RestService.ApiSunatDto;

namespace OpenInvoicePeru.RestService
{
    public interface IValidezComprobanteHelper
    {
        BaseResponseDto<TokenResponseDto> GenerarToken(string clientId, string clientSecret);
        ValidacionResponse Validar(string rucReceptor, string token, ValidacionRequest request);
        BaseResponseDto<TokenResponseDto> GenerarTokenSunat(string clientId, string clientSecret, string userName, string password);

    }
}