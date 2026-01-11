using ASP_NET_03._Dependency_Injection___Services;

namespace ASP_NET_03._Dependency_Injection___Services.Data;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public IEnumerable<Product> GetProducts();
}
