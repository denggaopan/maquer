using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.Catalog.Entities
{
    public class ProductCategory : BaseEntity<string>
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public string Description { get; set; }
        public string ParentCategoryId { get; set; }
        public bool Published { get; set; }
        public int Sort { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.SubName).HasMaxLength(150);
            builder.Property(x => x.Description).HasMaxLength(4000);
            builder.Property(x => x.ParentCategoryId).HasMaxLength(128);
        }
    }
}
