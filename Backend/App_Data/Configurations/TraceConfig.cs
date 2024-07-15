using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.App_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.App_Data.Configurations
{
    public class TraceConfig : IEntityTypeConfiguration<Trace>
    {
        public void Configure(EntityTypeBuilder<Trace> builder)
        {
            builder.Property(p => p.TraceIdentifier).IsRequired();
            builder.HasIndex(e => e.TraceIdentifier).IsUnique(true);            
            builder.Property(i => i.TraceIdentifier).HasColumnType("text").HasMaxLength(36);
        }
    }
}