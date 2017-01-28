using System.IO;

namespace DSpeckmann.SharpSrv
{
    class FileRequestHandler : RequestHandler
    {
        public override HTTP.Response GetResponse(HTTP.Request request, Location location)
        {
            string filename = location.Target + request.Path.Remove(0, location.Path.Length);

            if (!File.Exists(filename))
            {
                // TODO: Improve
                filename = filename + "/index.html";
                if (!File.Exists(filename))
                {
                    return new HTTP.Response(HTTP.Status.NotFound);
                }
            }

            // TODO: Add MIME support
            string body = File.ReadAllText(filename);
            return new HTTP.Response(HTTP.Status.OK)
            {
                Body = body
            };
        }
    }
}
