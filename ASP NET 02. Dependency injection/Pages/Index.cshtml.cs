using ASP_NET_02._Dependency_injection.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_02._Dependency_injection.Pages;

public class IndexModel : PageModel
{
    private readonly IProductRepository _repository;

    public IndexModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public void OnGet()
    {
        var products = _repository.GetProducts();
        ViewData["Products"] = products;
    }
}
