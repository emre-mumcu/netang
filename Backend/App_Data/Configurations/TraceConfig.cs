using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.App_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.App_Data.Configurations
{
    public class TraceConfig : IEntityTypeConfiguration<TraceEntity>
    {
        public void Configure(EntityTypeBuilder<TraceEntity> builder)
        {
            builder.Property(p => p.TraceID).IsRequired();
            builder.HasIndex(e => e.TraceID).IsUnique(true);            
            builder.Property(i => i.TraceID).HasColumnType("text").HasMaxLength(36);
        }
    }
}