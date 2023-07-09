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
    public class TrafficFineReasonConfiguration : IEntityTypeConfiguration<TrafficFineReason>
    {
        public void Configure(EntityTypeBuilder<TrafficFineReason> builder)
        {
            builder.Property(reason => reason.Price).HasColumnType("decimal(18,2)");

        }
    }
}
