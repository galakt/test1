using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppK.DataModels
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public bool? AdvertisingOptIn { get; set; }

        [DataMember]
        public string CountryIsoCode { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public string Locale { get; set; }
    }
}
