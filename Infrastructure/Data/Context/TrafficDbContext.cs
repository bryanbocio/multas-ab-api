
using Core.Entities.Agent;
using Core.Entities.Driver;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
