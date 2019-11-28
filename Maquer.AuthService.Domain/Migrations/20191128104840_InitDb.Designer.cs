﻿// <auto-generated />
using System;
using Maquer.AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maquer.AuthService.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191128104840_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Maquer.AuthService.Domain.Entities.ApiResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("ApiResource");
                });

            modelBuilder.Entity("Maquer.AuthService.Domain.Entities.AppClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessTokenLifetime");

                    b.Property<string>("AllowedGrantTypes")
                        .HasMaxLength(500);

                    b.Property<string>("AllowedScopes")
                        .HasMaxLength(500);

                    b.Property<string>("ClientId")
                        .HasMaxLength(50);

                    b.Property<string>("ClientSecrets")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("AppClient");
                });
#pragma warning restore 612, 618
        }
    }
}
