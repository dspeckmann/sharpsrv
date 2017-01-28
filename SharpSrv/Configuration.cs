using Newtonsoft.Json;
using System.IO;

namespace DSpeckmann.SharpSrv
{
    class Configuration
    {
        private Configuration() { }
         
        public static Configuration Load(string filename)
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(filename));
        }

        // TODO: Log file locations
        public bool DebugMode { get; set; } = false;
        public ServerConfiguration[] Servers { get; set; }
    }
}
