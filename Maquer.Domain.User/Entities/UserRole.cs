using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.User.Entities
{
    public class UserRole : BaseEntity<string>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
        }
    }
}
