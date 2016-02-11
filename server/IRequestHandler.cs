using System.Net;

namespace server
{
    internal interface IRequestHandler
    {
        void HandleRequest(HttpListenerContext context);
    }
}
