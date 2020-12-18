using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EntityMappers
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.RefreshToken);
            builder.HasData(
                new User { Id = Guid.NewGuid(), Username = "Ryan", Password = "12345" }
            );

        }
    }
}
