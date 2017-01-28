using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DSpeckmann.SharpSrv
{
    class SharpSrv
    {
        static void Main(string[] args)
        {
            Configuration configuration = Configuration.Load("config.json");
            Logger logger = new Logger(configuration);
            logger.Log(LogLevel.INFO, "SharpSrv 0.0.1 started");

            foreach (ServerConfiguration serverConfiguration in configuration.Servers)
            {
                new Thread(() =>
                {
                    new Server(serverConfiguration, logger).Run();
                })
                .Start();
            }
        }
    }
}
