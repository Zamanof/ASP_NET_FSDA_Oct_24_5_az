using ASP_NET_02._Dependency_injection.Models;

namespace ASP_NET_02._Dependency_injection.Data;
public class InMemoryRepository :IProductRepository
{
    private readonly IDictionary<Guid, Product> _products =
        new Dictionary<Guid, Product>();

    public InMemoryRepository()
    {
        AddProduct(new Product { Name = "Gence dovqasi", Description = "Bol noxudlu super dovqa" });
        AddProduct(new Product { Name = "Naxchivan qavurmasi", Description = "Yayda da qishda da ye getsin" });
        AddProduct(new Product { Name = "Aqsu qurutu", Description = "Sarimsaq qat xengele elave et" });
        AddProduct(new Product { Name = "Sumqayit zavod choreyi", Description = "Kalori qarantidir" });
        AddProduct(new Product { Name = "Qazax xengeli", Description = "Yarpaq xengeli" });
        AddProduct(new Product { Name = "Berde xichini", Description = "Lezzet" });
    }

    public Product AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        _products.Add(product.Id, product);
        return product;
    }

    public IEnumerable<Product> GetProducts() => _products.Values;
}
