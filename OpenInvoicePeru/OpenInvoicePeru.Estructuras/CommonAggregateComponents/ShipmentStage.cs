using System;
using System.IO;
using OpenInvoicePeru.Estructuras.CommonBasicComponents;
using OpenInvoicePeru.Estructuras.SunatAggregateComponents;

namespace OpenInvoicePeru.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class ShipmentStage
    {
        public int Id { get; set; }

        public CarrierParty CarrierParty { get; set; }

        //public PartyIdentification DriverPerson { get; set;
        public DriverPerson DriverPerson { get; set; }

        public string TransportModeCode { get; set; }

        /// <remarks>
        /// cac:TransitPeriod/cbc:StartDate
        /// </remarks>>
        public DateTime TransitPeriodStartPeriod { get; set; }

        public SunatRoadTransport TransportMeans { get; set; }

        public ShipmentStage()
        {
            //DriverPerson = new PartyIdentification();
            DriverPerson = new DriverPerson();
            TransportMeans = new SunatRoadTransport();
        }
    }
}