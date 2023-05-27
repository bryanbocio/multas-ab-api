
using Core.Entities.TrafficFine;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class TrafficDbContext : DbContext
    {
        public TrafficDbContext(DbContextOptions<TrafficDbContext> options): base(options)
        {

        }
        public DbSet<TrafficFine> TrafficFine { get; set; }

    }
}
