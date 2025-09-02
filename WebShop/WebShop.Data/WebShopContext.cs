using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Domain;

namespace WebShop.Data
{
    public class WebShopContext : DbContext
    {
        public DbSet<Spaceships> Spaceships { get; set; }
    }
}
