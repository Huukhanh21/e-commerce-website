using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class OrderDetailContext : DbContext
    {
        public OrderDetailContext(DbContextOptions<OrderDetailContext> options) : base(options)
        {
        }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
