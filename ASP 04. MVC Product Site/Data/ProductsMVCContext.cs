using ASP_04._MVC_Product_Site.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_04._MVC_Product_Site.Data;

public class ProductsMVCContext : DbContext
{
    public ProductsMVCContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
