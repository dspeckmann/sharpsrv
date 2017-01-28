using System;
using System.Collections.Generic;

namespace DSpeckmann.SharpSrv.HTTP
{
    class Request
    {
        public RequestMethod Method { get; set; }
        public string Path { get; set; }
        public List<KeyValuePair<string, string>> HeaderFields { get; set; }
        public string Body { get; set; }

        public Request()
        {
            HeaderFields = new List<KeyValuePair<string, string>>();
        }

        public Request(string raw)
        {
            string[] requestParts = raw.Split(new [] { "\r\n\r\n" }, StringSplitOptions.None);
            if(requestParts.Length != 2)
            {
                // TODO: Throw better exception
                throw new Exception("HTTP request has fewer or more parts than 2 (header and body)!");
            }

            string header = requestParts[0];
            Body = requestParts[1];

            string[] headerLines = header.Split(new [] { "\r\n" }, StringSplitOptions.None);
            string[] headerParts = headerLines[0].Split(' ');
            if(headerParts.Length != 3)
            {
                // TODO: Throw better exception
                throw new Exception("HTTP request header has fewer or more parts than 3 (method, path, version)!");
            }

            if(headerParts[2].Trim() != "HTTP/1.0" && headerParts[2].Trim() != "HTTP/1.1")
            {
                // TODO: Throw better exception (and actually handle it)
                // TODO: Or just add HTTP2 support ;)
                throw new Exception("Unsupported HTTP version!");
            }

            Method = (RequestMethod)Enum.Parse(typeof(RequestMethod), headerParts[0].Trim());
            Path = headerParts[1].Trim();

            HeaderFields = new List<KeyValuePair<string, string>>();
            for(int i = 1; i < headerLines.Length; i++)
            {
                string[] headerFieldParts = headerLines[i].Split(new [] { ':' }, 2);
                if(headerFieldParts.Length != 2)
                {
                    // TODO: yada yada yada
                    // TODO: Or just ignore this particular header field and continue?
                    throw new Exception("Invalid HTTP request header field!");
                }
                HeaderFields.Add(new KeyValuePair<string, string>(headerFieldParts[0].Trim(), headerFieldParts[1].Trim()));
            }
        }
    }
}
