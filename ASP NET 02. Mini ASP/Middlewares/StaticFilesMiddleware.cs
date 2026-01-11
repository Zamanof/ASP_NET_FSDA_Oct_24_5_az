using ASP_NET_02._Mini_ASP.Interfaces;
using System.Net;

namespace ASP_NET_02._Mini_ASP.Middlewares;

class StaticFilesMiddleware : IMiddleware
{
    private readonly string BASE_PATH = @"..\..\..\wwwroot\";
    public HttpHandler Next { get; set; }

    public void Handle(HttpListenerContext context)
    {
        if (Path.HasExtension(context.Request.RawUrl))
        {
            try
            {
                var fileName = context.Request.RawUrl.Substring(1);
                var path = @$"{BASE_PATH}{fileName}";
                var bytes = File.ReadAllBytes(path);
                if (Path.GetExtension(path) == ".html")
                {
                    context.Response.AddHeader("Content-Type", "text/html");
                }
                else if (Path.GetExtension(path) == ".png")
                {
                    context.Response.AddHeader("Content-Type", "image/png");
                }
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "Page or file not found";
                var notfound = File.ReadAllBytes($"{BASE_PATH}404.html");
                context.Response.AddHeader("Content-Type", "text/html");
                context.Response.OutputStream.Write(notfound, 0, notfound.Length);
            }
        }
        else Next.Invoke(context);

        context.Response.Close();
    }
}
