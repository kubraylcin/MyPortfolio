using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Migrations
{
    public partial class AddImageEntityAndUpdatePortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Portfolios");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Portfolios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_ImageId",
                table: "Portfolios",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Images_ImageId",
                table: "Portfolios",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Images_ImageId",
                table: "Portfolios");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_ImageId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Portfolios");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
