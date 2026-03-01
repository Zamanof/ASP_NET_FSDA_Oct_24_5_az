using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;


namespace ASP_NET_22._TaskFlowCQRS_Integration_Test;

public class CustomWebApplicationFactory: WebApplicationFactory<global::Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, config) =>
        
            config.AddInMemoryCollection(new Dictionary<string, string> { ["UseInMemoryDatabase"]= "True"}!)
        );
    }
}
