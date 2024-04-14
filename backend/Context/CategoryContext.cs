using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
