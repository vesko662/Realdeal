using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Realdeal.Data.Migrations
{
    public partial class AddedUserReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advert_AspNetUsers_UserId",
                table: "Advert");

            migrationBuilder.DropForeignKey(
                name: "FK_Advert_SubCategories_SubCategoryId",
                table: "Advert");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertImages_Advert_AdvertId",
                table: "AdvertImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Advert_AdvertId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservedAdverts_Advert_AdvertId",
                table: "ObservedAdverts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReporedAdverts_Advert_AdvertId",
                table: "ReporedAdverts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advert",
                table: "Advert");

            migrationBuilder.RenameTable(
                name: "Advert",
                newName: "Adverts");

            migrationBuilder.RenameIndex(
                name: "IX_Advert_UserId",
                table: "Adverts",
                newName: "IX_Adverts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Advert_SubCategoryId",
                table: "Adverts",
                newName: "IX_Adverts_SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adverts",
                table: "Adverts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ReporedUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMakerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReportedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedUserUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporedUsers_Adverts_UserMakerId",
                        column: x => x.UserMakerId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReporedUsers_AspNetUsers_ReportedUserUserId",
                        column: x => x.ReportedUserUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReporedUsers_ReportedUserUserId",
                table: "ReporedUsers",
                column: "ReportedUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReporedUsers_UserMakerId",
                table: "ReporedUsers",
                column: "UserMakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertImages_Adverts_AdvertId",
                table: "AdvertImages",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_AspNetUsers_UserId",
                table: "Adverts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_SubCategories_SubCategoryId",
                table: "Adverts",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Adverts_AdvertId",
                table: "Messages",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObservedAdverts_Adverts_AdvertId",
                table: "ObservedAdverts",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReporedAdverts_Adverts_AdvertId",
                table: "ReporedAdverts",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertImages_Adverts_AdvertId",
                table: "AdvertImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_AspNetUsers_UserId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_SubCategories_SubCategoryId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Adverts_AdvertId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservedAdverts_Adverts_AdvertId",
                table: "ObservedAdverts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReporedAdverts_Adverts_AdvertId",
                table: "ReporedAdverts");

            migrationBuilder.DropTable(
                name: "ReporedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adverts",
                table: "Adverts");

            migrationBuilder.RenameTable(
                name: "Adverts",
                newName: "Advert");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_UserId",
                table: "Advert",
                newName: "IX_Advert_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_SubCategoryId",
                table: "Advert",
                newName: "IX_Advert_SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advert",
                table: "Advert",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_AspNetUsers_UserId",
                table: "Advert",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_SubCategories_SubCategoryId",
                table: "Advert",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertImages_Advert_AdvertId",
                table: "AdvertImages",
                column: "AdvertId",
                principalTable: "Advert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Advert_AdvertId",
                table: "Messages",
                column: "AdvertId",
                principalTable: "Advert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObservedAdverts_Advert_AdvertId",
                table: "ObservedAdverts",
                column: "AdvertId",
                principalTable: "Advert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReporedAdverts_Advert_AdvertId",
                table: "ReporedAdverts",
                column: "AdvertId",
                principalTable: "Advert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
