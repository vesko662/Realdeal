using Microsoft.EntityFrameworkCore.Migrations;

namespace Realdeal.Data.Migrations
{
    public partial class FeedbackLinckedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MakerId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_MakerId",
                table: "Feedbacks",
                column: "MakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_MakerId",
                table: "Feedbacks",
                column: "MakerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_MakerId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_MakerId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "MakerId",
                table: "Feedbacks");
        }
    }
}
