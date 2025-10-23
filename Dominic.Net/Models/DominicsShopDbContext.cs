using Microsoft.EntityFrameworkCore;
namespace Dominic.Net.Models
{
    public class DominicShopDbContext : DbContext
    {
        public DominicShopDbContext(DbContextOptions<DominicShopDbContext> options) : base(options)
        {
        }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}