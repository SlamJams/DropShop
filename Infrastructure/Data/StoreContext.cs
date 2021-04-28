using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Reflection;
using System.Linq;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    

        //this method is responsible for creating our migrations. This will ovveride the default method in order to look for our configurations.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var type in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = type.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var prop in properties)
                    {
                        modelBuilder.Entity(type.Name).Property(prop.Name).HasConversion<double>();
                    }
                }
            }
        }
    
    }
}