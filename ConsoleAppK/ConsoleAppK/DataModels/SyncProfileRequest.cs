using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppK.DataModels
{
    public class SyncProfileRequest : MyAccountRequestBase
    {
        public bool? AdvertisingOptIn { get; set; }

        public string CountryIsoCode { get; set; }

        public DateTime DateModified { get; set; }

        public string Locale { get; set; }
    }
}
