namespace ASP_NET_03._Dependency_Injection___Services.Data;
public class InMemoryRepository :IProductRepository
{
    private readonly IDictionary<Guid, Product> _products =
        new Dictionary<Guid, Product>();

    public InMemoryRepository(string prefix)
    {
        AddProduct(new Product { Name = $"{prefix} Gence dovqasi", Description = "Bol noxudlu super dovqa" });
        AddProduct(new Product { Name = $"{prefix} Naxchivan qavurmasi", Description = "Yayda da qishda da ye getsin" });
        AddProduct(new Product { Name = $"{prefix} Aqsu qurutu", Description = "Sarimsaq qat xengele elave et" });
        AddProduct(new Product { Name = $"{prefix} Sumqayit zavod choreyi", Description = "Kalori qarantidir" });
        AddProduct(new Product { Name = $"{prefix} Qazax xengeli", Description = "Yarpaq xengeli" });
        AddProduct(new Product { Name = $"{prefix} Berde xichini", Description = "Lezzet" });
    }

    public Product AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        _products.Add(product.Id, product);
        return product;
    }

    public IEnumerable<Product> GetProducts() => _products.Values;
}
