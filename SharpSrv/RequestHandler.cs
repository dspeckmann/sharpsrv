using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSpeckmann.SharpSrv
{
    abstract class RequestHandler
    {
        public abstract HTTP.Response GetResponse(HTTP.Request request, Location location);
    }
}
