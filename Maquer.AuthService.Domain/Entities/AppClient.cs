using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.AuthService.Domain.Entities
{
    public class AppClient : BaseEntity<int>
    {
        public string ClientId { get; set; }
        public string AllowedGrantTypes { get; set; }
        public string ClientSecrets { get; set; }
        public string AllowedScopes { get; set; }
        public int AccessTokenLifetime { get; set; } = 3600;
    }

    public class AppClientConfiguration : IEntityTypeConfiguration<AppClient>
    {
        public void Configure(EntityTypeBuilder<AppClient> builder)
        {
            builder.ToTable("AppClient");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseSqlServerIdentityColumn();
            builder.Property(x => x.ClientId).HasMaxLength(50);
            builder.Property(x => x.AllowedGrantTypes).HasMaxLength(500);
            builder.Property(x => x.ClientSecrets).HasMaxLength(500);
            builder.Property(x => x.AllowedScopes).HasMaxLength(500);
        }
    }
}
