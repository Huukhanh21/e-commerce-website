using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
    }
}
