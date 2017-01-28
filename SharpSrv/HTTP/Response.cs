using System.Collections.Generic;
using System.Text;

namespace DSpeckmann.SharpSrv.HTTP
{
    class Response
    {
        public Status Status { get; set; }
        public List<KeyValuePair<string, string>> HeaderFields { get; private set; }

        public string Body { get; set; }
        
        public Response(Status status)
        {
            Status = status;
            HeaderFields = new List<KeyValuePair<string, string>>();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("HTTP/1.1 " + Status + "\r\n");
            foreach(KeyValuePair<string, string> headerField in HeaderFields)
            {
                builder.Append(headerField.Key + ": " + headerField.Value + "\r\n");
            }
            builder.Append("\r\n" + Body);
            return builder.ToString();
        }
    }
}
