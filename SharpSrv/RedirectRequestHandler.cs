using DSpeckmann.SharpSrv.HTTP;
using System.Collections.Generic;

namespace DSpeckmann.SharpSrv
{
    class RedirectRequestHandler : RequestHandler
    {
        public override Response GetResponse(Request request, Location location)
        {
            string newPath = request.Path.Replace(location.Path, location.Target);
            Response response = new Response(HTTP.Status.MovedPermanently);
            response.HeaderFields.Add(new KeyValuePair<string, string>("Location", newPath)); // TODO: Add host to path.
            return response;
        }
    }
}
