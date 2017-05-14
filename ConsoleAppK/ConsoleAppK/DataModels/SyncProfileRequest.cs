using System;
using ConsoleAppK.Validation;

namespace ConsoleAppK.DataModels
{
    public class SyncProfileRequest : MyAccountRequestBase
    {
        public bool? AdvertisingOptIn { get; set; }

        // ISO 3166 two-letter uppercase subculture code associated with a country or region
        [IsoCountryCode]
        public string CountryIsoCode { get; set; }

        public DateTime DateModified { get; set; }

        // Combination of an ISO 639 two-letter lowercase culture code associated with a language 
        // and an ISO 3166 two-letter uppercase subculture code associated with a country or region
        [Locale]
        public string Locale { get; set; }
    }
}
