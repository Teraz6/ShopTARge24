using Microsoft.EntityFrameworkCore;
using System;

namespace WebShop.Data
{
    public class KindergardenDbContext : DbContext
    {
        public KindergardenDbContext(DbContextOptions<KindergardenDbContext> options) : base(options) { }

        public DbSet<Kindergarden> Kindergardens { get; set; }
    }
}
