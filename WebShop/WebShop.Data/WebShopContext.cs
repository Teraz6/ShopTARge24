using Microsoft.EntityFrameworkCore;
using WebShop.Core.Domain;

namespace WebShop.Data
{
    public class WebShopContext : DbContext
    {
        public WebShopContext(DbContextOptions<WebShopContext> options)
            : base(options) { }

        public DbSet<Spaceships> Spaceships { get; set; }
        public DbSet<Kindergarden> Kindergardens { get; set; }

        public DbSet<FileToApi> FileToApis { get; set; }
    }
}
