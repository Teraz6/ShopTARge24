using Microsoft.EntityFrameworkCore;
using WebShop.Core.Domain;

namespace WebShop.Data
{
    public class WebShopContext : DbContext
    {
        public WebShopContext(DbContextOptions<WebShopContext> options)
            : base(options) { }

        public DbSet<Spaceships> Spaceships { get; set; }
    }
}
