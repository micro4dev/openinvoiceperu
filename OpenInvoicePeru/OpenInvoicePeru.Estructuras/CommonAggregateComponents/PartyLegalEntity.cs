using System;

namespace OpenInvoicePeru.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class PartyLegalEntity
    {
        public string RegistrationName { get; set; }
        public string CompanyId { get; set; }
        public RegistrationAddress RegistrationAddress { get; set; }

        public PartyLegalEntity()
        {
            RegistrationAddress = new RegistrationAddress();
            CompanyId = "0001"; //NroMtc
        }
    }
}