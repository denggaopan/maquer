using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.User.Entities
{
    public class RoleApiUrl : BaseEntity<string>
    {
        public string RoleId { get; set; }
        public string ApiUrlId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ApiUrl ApiUrl { get; set; }

    }

    public class RoleApiUrlConfiguration : IEntityTypeConfiguration<RoleApiUrl>
    {
        public void Configure(EntityTypeBuilder<RoleApiUrl> builder)
        {
            builder.ToTable("RoleApiUrl");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
        }
    }
}
