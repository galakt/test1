using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppK.WCF
{
    public class UserNotFound
    {
        public UserNotFound()
        {
            Message = "UserNotFound";
        }

        public UserNotFound(string message)
        {
            Message = message;
        }

        [DataMember]
        public string Message { get; set; }
    }
}
