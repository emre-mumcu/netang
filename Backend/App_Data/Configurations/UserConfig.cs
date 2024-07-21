using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.App_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.App_Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired();
            builder.HasIndex(e => e.UserName).IsUnique(true);
            builder.Property(i => i.UserName).HasColumnType("text").HasMaxLength(12);
        }
    }
}