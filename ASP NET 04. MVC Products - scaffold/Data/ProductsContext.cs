using ASP_NET_04._MVC_Products___scaffold.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_04._MVC_Products___scaffold.Data;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
