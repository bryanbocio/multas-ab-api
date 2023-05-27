using Core.Entities.TrafficFine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    internal class TrafficFineConfiguration : IEntityTypeConfiguration<TrafficFine>
    {
        public void Configure(EntityTypeBuilder<TrafficFine> builder)
        {
            builder.Property(trafficFine => trafficFine.Id).IsRequired();
            builder.Property(trafficFine => trafficFine.CarPlate).IsRequired();
            builder.Property(trafficFine => trafficFine.Reason).IsRequired().HasMaxLength(200);
            builder.Property(trafficFine => trafficFine.Latitude).IsRequired();
            builder.Property(trafficFine => trafficFine.Longitude).IsRequired();

            builder.HasOne(trafficFine => trafficFine.Driver).WithMany().HasForeignKey(Fine => Fine.DriverId);
            builder.HasOne(trafficFine => trafficFine.Agent).WithMany().HasForeignKey(Fine => Fine.AgentId);
        }
    }
}
