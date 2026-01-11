using ASP_NET_03._Dependency_Injection___Services.Data;
using ASP_NET_03._Dependency_Injection___Services.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_03._Dependency_Injection___Services.Pages;

public class IndexModel : PageModel
{
    private readonly ProductService _service;

    public IndexModel(ProductService service)
    {
        _service = service;
    }

    public void OnGet()
    {
        var products = _service.GetProducts();
        ViewData["Products"] = products;
    }
}
