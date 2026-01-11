using ASP_NET_03._Razor_Pages_Product_site.Models;
using System.Collections;

namespace ASP_NET_03._Razor_Pages_Product_site.Data;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Product AddProduct(Product product);
}
