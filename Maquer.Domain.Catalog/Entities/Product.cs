using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.Catalog.Entities
{
    public class Product : BaseEntity<string>
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsRecommend { get; set; }
        public bool IsHot { get; set; }
        public bool IsTop { get; set; }
        public bool IsSald { get; set; }

        public virtual ICollection<Sku> Skus { get; set; }

    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.SubName).HasMaxLength(150);
            builder.Property(x => x.Description).HasMaxLength(4000);
            builder.Property(x => x.Tags).HasMaxLength(150);
            builder.Property(x => x.ImgUrl).HasMaxLength(150);
        }
    }
}
