using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.Catalog.Entities
{
    public class SkuItem : BaseEntity<string>
    {
        public virtual Sku Sku { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual ProductAttributeItem ProductAttributeItem { get; set; }
    }

    public class SkuItemConfiguration : IEntityTypeConfiguration<SkuItem>
    {
        public void Configure(EntityTypeBuilder<SkuItem> builder)
        {
            builder.ToTable("SkuItem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
        }
    }
}
