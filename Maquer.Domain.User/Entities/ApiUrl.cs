using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.User.Entities
{
    public class ApiUrl : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }

    }

    public class ApiUrlConfiguration : IEntityTypeConfiguration<ApiUrl>
    {
        public void Configure(EntityTypeBuilder<ApiUrl> builder)
        {
            builder.ToTable("ApiUrl");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Code).HasMaxLength(50);
            builder.Property(x => x.Url).HasMaxLength(150);
            builder.Property(x => x.Method).HasMaxLength(50);
        }
    }
}
