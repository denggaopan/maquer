using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.Catalog.Entities
{
    public class ProductAttributeItem : BaseEntity<string>
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string ImgUrl { get; set; }

        public int Sort { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }
    }

    public class ProductAttributeItemConfiguration : IEntityTypeConfiguration<ProductAttributeItem>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeItem> builder)
        {
            builder.ToTable("ProductAttributeItem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
