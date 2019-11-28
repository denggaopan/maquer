using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.AuthService.Domain.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<AppClient> AppClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApiResourceConfiguration());
            modelBuilder.ApplyConfiguration(new AppClientConfiguration());
        }
    }
}
