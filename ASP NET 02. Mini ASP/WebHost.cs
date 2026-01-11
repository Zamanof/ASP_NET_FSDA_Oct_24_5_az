using ASP_NET_02._Mini_ASP.Interfaces;
using System.Net;

namespace ASP_NET_02._Mini_ASP;

class WebHost
{
    private int _port;
    private HttpHandler _handler;
    private HttpListener _listener;
    private MiddlewareBuilder _builder = new();

    public WebHost(int port)
    {
        _port = port;
        _listener = new();
        _listener.Prefixes.Add($"http://localhost:{_port}/");
    }
    public void HandlerRequest(HttpListenerContext context)
    {
        _handler.Invoke(context);
    }

    public void Run()
    {
        _listener.Start();
        Console.WriteLine($"Server start at {_port}");
        while (true)
        {
            HttpListenerContext context = _listener.GetContext();
            Task.Run(() => HandlerRequest(context));
        }
    }

    public void UseStartup<T>() where T: IStartup, new()
    {
        IStartup startup = new T();
        startup.Configure(_builder);
        _handler = _builder.Build();
    }
}
