using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Repository.Entities;

namespace Order.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Orders> orders { get; set; }   
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Product> products { get; set; }
      

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("User", "Identity");
            builder.Entity<IdentityRole>().ToTable("Role", "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Identity");
        }
    }
}
