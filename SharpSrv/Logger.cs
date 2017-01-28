using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSpeckmann.SharpSrv
{
    class Logger
    {
        private Configuration configuration;

        public Logger(Configuration configuration)
        {
            this.configuration = configuration;
        }

        // TODO: Get server name automatically?
        public void Log(LogLevel level, string message, string server = null)
        {
            if(level == LogLevel.DEBUG && !configuration.DebugMode)
            {
                return;
            }
            
            Console.WriteLine("[{0}] [{1}]{2} {3}", DateTime.Now, level, (server == null) ? "" : string.Format(" [{0}]", server), message);
            // TODO: Write to log files
        }
    }
}
