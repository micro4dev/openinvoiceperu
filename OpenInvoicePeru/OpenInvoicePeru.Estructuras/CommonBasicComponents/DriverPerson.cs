using System;
using System.IO;

namespace OpenInvoicePeru.Estructuras.CommonBasicComponents
{
    [Serializable]
    public class DriverPerson
    {
        public PartyIdentificationId DriverIdentificationId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string JobTitle { get; set; }    
        public string IdentityDocumentReference { get; set; }

        public DriverPerson()
        {
            DriverIdentificationId = new PartyIdentificationId();
        }
    }
}