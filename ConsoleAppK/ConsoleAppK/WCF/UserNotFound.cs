using System.Runtime.Serialization;

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
