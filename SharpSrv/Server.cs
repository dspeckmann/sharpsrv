using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DSpeckmann.SharpSrv
{
    class Server
    {
        private ServerConfiguration serverConfiguration;
        private Logger logger;

        public Server(ServerConfiguration serverConfiguration, Logger logger)
        {
            this.serverConfiguration = serverConfiguration;
            this.logger = logger;
        }

        public void Run()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(serverConfiguration.IP), serverConfiguration.Port);
            listener.Start();

            Byte[] buffer = new Byte[255];
            String data = null;

            logger.Log(LogLevel.INFO, string.Format("Server listening at {0}", listener.LocalEndpoint), serverConfiguration.Name);

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                var clientThread = new Thread(() =>
                {
                    NetworkStream stream = client.GetStream();
                    int i;
                    data = null;
                    string requestString = null;
                    while (stream.DataAvailable && (i = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        data = Encoding.ASCII.GetString(buffer, 0, i);
                        requestString += data;
                    }

                    if (string.IsNullOrEmpty(requestString))
                    {
                        client.Close();
                        return;
                    }

                    HTTP.Request request = new HTTP.Request(requestString);
                    logger.Log(LogLevel.ACCESS, string.Format("Request from {0}: {1} {2} HTTP/1.1", client.Client.RemoteEndPoint, request.Method, request.Path), serverConfiguration.Name);
                    HTTP.Response response = null;
                    try
                    {
                        response = GetResponse(request);
                    }
                    catch(Exception)
                    {
                        response = new HTTP.Response(HTTP.Status.InternalServerError); // TODO: What status code?
                    }
                    byte[] responseBytes = Encoding.ASCII.GetBytes(response.ToString());
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    logger.Log(LogLevel.ACCESS, string.Format("Response to {0}: HTTP/1.1 {1}", client.Client.RemoteEndPoint, response.Status), serverConfiguration.Name);
                    client.Close();
                });
                clientThread.Start();
            }
        }

        private HTTP.Response GetResponse(HTTP.Request request)
        {
            Location currentLocation = null;
            foreach (Location location in serverConfiguration.Locations.OrderByDescending(l => l.Path.Length))
            {
                if (request.Path.StartsWith(location.Path))
                {
                    currentLocation = location;
                    break;
                }
            }

            // TODO: Add Authorization

            switch (currentLocation)
            {
                case Location l when l.Action == "Serve":
                    return new FileRequestHandler().GetResponse(request, currentLocation);
                case Location l when l.Action == "Redirect":
                    return new RedirectRequestHandler().GetResponse(request, currentLocation);
                case Location l when l.Action == "Proxy":
                    return new ProxyRequestHandler().GetResponse(request, currentLocation);
            }
            
            // TODO: Throw customized exception (Something like ServerConfigException? BadHandlerException?)
            throw new Exception("Invalid request handler");
        }
    }
}
