using ASP_NET_03._Razor_Pages_Product_site.Data;
using ASP_NET_03._Razor_Pages_Product_site.Models;
using ASP_NET_03._Razor_Pages_Product_site.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_03._Razor_Pages_Product_site.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ProductService _service;
        public IEnumerable<Product> Products { get; set; }

        public ProductsModel(ProductService service)
        {
            _service = service;
        }

        public async void OnGet()
        {
             Products = await _service.GetProductsAsync();
        }
    }
}
