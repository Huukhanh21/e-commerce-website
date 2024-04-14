using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; } // Thêm định nghĩa cho DbSet<Product> ở đây

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa các quan hệ, khóa ngoại, hoặc các chỉ mục tại đây (nếu cần)
        }
    }
}
