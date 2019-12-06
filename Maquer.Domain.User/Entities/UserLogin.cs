using Maquer.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.User.Entities
{
    public class UserLogin : BaseEntity<string>
    {
        public string Token { get; set; }
        public int Expires { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogin");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Token).HasMaxLength(1024);
        }
    }
}
