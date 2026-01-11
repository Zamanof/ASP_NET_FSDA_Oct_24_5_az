using ASP_NET_03._Razor_Pages_Product_site.Models;
using ASP_NET_03._Razor_Pages_Product_site.Services;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_03._Razor_Pages_Product_site.Pages;

public class IndexModel : PageModel
{
    private readonly ProductService _service;

    public IndexModel(ProductService service)
    {
        _service = service;
    }

    //public void OnPost(string name, string description, uint count, decimal price)
    //{
    //    var product = new Product()
    //    {
    //        Name = name,
    //        Description = description,
    //        Count = count,
    //        Price = price
    //    };
    //    _service.AddProduct(product);
    //}

    public void OnPost(Product product)
    {
        _service.AddProduct(product);
    }
}
