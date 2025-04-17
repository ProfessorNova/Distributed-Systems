using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;

namespace ShoppingApi.Data
{
    /// <summary>
    /// EF Core context for shopping items.
    /// </summary>
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
        }

        /// <summary>Items table.</summary>
        public DbSet<Item> Items => Set<Item>();
    }
}
