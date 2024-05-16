using Microsoft.EntityFrameworkCore;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}