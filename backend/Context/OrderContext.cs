using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
