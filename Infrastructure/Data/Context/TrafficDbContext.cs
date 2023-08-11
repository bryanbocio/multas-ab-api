
using Core.Entities.Agent;
using Core.Entities.Driver;
using Core.Entities.OrderAgregate;
using Core.Entities.TrafficFine;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data.Context
{
    public class TrafficDbContext : DbContext
    {
        public TrafficDbContext(DbContextOptions<TrafficDbContext> options): base(options)
        {

        }
        public DbSet<TrafficFine> TrafficFine { get; set; }
        public DbSet<Agent> Agent{ get; set; }
        public DbSet<Driver> Driver{ get; set; }
        public DbSet<TrafficFineReason> TrafficFineReasons { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}
