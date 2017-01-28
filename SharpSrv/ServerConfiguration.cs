using Newtonsoft.Json;

namespace DSpeckmann.SharpSrv
{
    [JsonObject(Title = "Server")]
    class ServerConfiguration
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public Location[] Locations { get; set; }
    }
}
