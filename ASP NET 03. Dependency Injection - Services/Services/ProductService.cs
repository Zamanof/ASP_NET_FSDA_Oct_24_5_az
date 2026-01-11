using ASP_NET_03._Dependency_Injection___Services.Data;

namespace ASP_NET_03._Dependency_Injection___Services.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Product AddProduct(Product product)
        => _repository.AddProduct(product);

    public IEnumerable<Product> GetProducts() 
        => _repository.GetProducts();

}
