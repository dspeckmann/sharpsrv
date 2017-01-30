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
            
            Console.Write("[{0}] ", DateTime.Now);
            ConsoleColor oldColor = Console.ForegroundColor;
            switch(level)
            {
                case LogLevel.ACCESS:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogLevel.INFO:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogLevel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.DEBUG:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.Write("[{0}] ", level);
            Console.ForegroundColor = oldColor;
            if(!string.IsNullOrEmpty(server))
            {
                Console.Write("[{0}] ", server);
            }
            Console.WriteLine("{0}", message);
            

            // TODO: Write to log files
        }
    }
}
