using ASP_NET_03._Razor_Pages_Product_site.Models;
using ASP_NET_03._Razor_Pages_Product_site.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_03._Razor_Pages_Product_site.Pages;

public class ProductModel : PageModel
{
    private readonly ProductService _service;
    public Product Product { get; set; }

    public ProductModel(ProductService service)
    {
        _service = service;
    }

    public async void OnGetAsync(int id)
    {
        Product = await _service.GetProductByIdAsync(id);
    }
}
