using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maquer.UserService.Domain.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    PasswordSalt = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumberConfirm = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    EmailConfirm = table.Column<bool>(nullable: false),
                    NickName = table.Column<string>(maxLength: 50, nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
