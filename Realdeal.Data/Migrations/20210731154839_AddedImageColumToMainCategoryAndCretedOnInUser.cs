using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Realdeal.Data.Migrations
{
    public partial class AddedImageColumToMainCategoryAndCretedOnInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Adverts");

            migrationBuilder.AddColumn<string>(
                name: "CategoryImage",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreteOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImage",
                table: "MainCategories");

            migrationBuilder.DropColumn(
                name: "CreteOn",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
