using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.AuthService.Domain.Entities
{
    public class ApiResource : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable("ApiResource");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseSqlServerIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}
