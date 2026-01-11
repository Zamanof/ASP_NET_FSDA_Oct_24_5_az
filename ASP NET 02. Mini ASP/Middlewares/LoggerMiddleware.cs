using ASP_NET_02._Mini_ASP.Interfaces;
using System.Net;

namespace ASP_NET_02._Mini_ASP.Middlewares;
class LoggerMiddleware : IMiddleware
{
    public HttpHandler Next { get; set; }

    public void Handle(HttpListenerContext context)
    {
        Console.WriteLine($"""
            method:     {context.Request.HttpMethod}
            url:        {context.Request.RawUrl}
            endpoint:   {context.Request.RemoteEndPoint}

            """);
        Next.Invoke(context);
    }
}
