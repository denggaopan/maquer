using Maquer.Common.Database;
using Maquer.Domain.Catalog.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.Catalog.Entities
{
    public class ProductAttribute : BaseEntity<string>
    {
        public string Name { get; set; }
        public ProductAttributeShowType ShowType { get; set; }
        public int Sort { get; set; }


        public virtual Product Product { get; set; }
        public virtual ICollection<ProductAttributeItem> ProductAttributeItems { get; set; }

    }

    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttribute");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
