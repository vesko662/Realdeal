using Microsoft.EntityFrameworkCore.Migrations;

namespace Realdeal.Data.Migrations
{
    public partial class ViewCounterOnAdvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "Adverts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Adverts");
        }
    }
}
