using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineWebStore.entity;

namespace OnlineWebStore.config
{
    public class StoreContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ProductOrder> productsOrder { get; set; }
        public DbSet<Store> stores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;Database=OnlineStore3; integrated security = true;trustservercertificate=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
