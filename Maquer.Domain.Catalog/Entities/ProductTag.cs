using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.Catalog.Entities
{
    public class ProductTag : BaseEntity<string>
    {
        public string Name { get; set; }
        public int Sort { get; set; }


    }

    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTag");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
