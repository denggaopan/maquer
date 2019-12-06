﻿// <auto-generated />
using System;
using Maquer.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maquer.Domain.User.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191130090232_UpdateUserLogin")]
    partial class UpdateUserLogin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Maquer.Domain.User.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<string>("Address")
                        .HasMaxLength(150);

                    b.Property<string>("Avatar")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("Birthday");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("EmailConfirm");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NickName")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasMaxLength(128);

                    b.Property<string>("PasswordSalt")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50);

                    b.Property<bool>("PhoneNumberConfirm");

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Maquer.Domain.User.Entities.UserLogin", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("Expires");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Token")
                        .HasMaxLength(1024);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Maquer.Domain.User.Entities.UserLogin", b =>
                {
                    b.HasOne("Maquer.Domain.User.Entities.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
