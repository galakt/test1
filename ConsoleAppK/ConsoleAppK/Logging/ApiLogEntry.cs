using System;

namespace ConsoleAppK.Logging
{
    public class ApiLogEntry
    {
        public string RequestContentBody { get; set; }      // The request content body.
        public string RequestUri { get; set; }              // The request URI.
        public string RequestMethod { get; set; }           // The request method (GET, POST, etc).
        public string RequestHeaders { get; set; }          // The request headers.
        public DateTime? RequestTimestamp { get; set; }     // The request timestamp.
        public string ResponseContentType { get; set; }     // The response content type.
        public string ResponseContentBody { get; set; }     // The response content body.
        public int? ResponseStatusCode { get; set; }        // The response status code.
        public string ResponseHeaders { get; set; }         // The response headers.
        public DateTime? ResponseTimestamp { get; set; }    // The response timestamp.

        public override string ToString()
        {
            return $"REQUEST {RequestTimestamp} {RequestMethod} {RequestUri} {RequestContentBody} {RequestHeaders} " +
                   $"RESPONSE {ResponseTimestamp} {ResponseStatusCode} {ResponseContentBody}";
        }
    }
}
