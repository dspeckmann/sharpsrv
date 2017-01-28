using System;
using System.Collections.Generic;

namespace DSpeckmann.SharpSrv.HTTP
{
    class Request
    {
        public RequestMethod Method { get; set; }
        public List<KeyValuePair<string, string>> HeaderFields { get; set; }
        public string Path { get; set; }

        public Request()
        {
            HeaderFields = new List<KeyValuePair<string, string>>();
        }

        public Request(string raw)
        {
            string[] headerSplit = raw.Split(' ');
            Method = (RequestMethod)Enum.Parse(typeof(RequestMethod), headerSplit[0]);
            Path = headerSplit[1];
            // TODO: Parse header correctly
        }
    }
}
