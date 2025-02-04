using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Migrations
{
    public partial class AddImageColumnToFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameSurname",
                table: "Testimonials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_ImageId",
                table: "Features",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Images_ImageId",
                table: "Features",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Images_ImageId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_ImageId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Features");

            migrationBuilder.AlterColumn<int>(
                name: "NameSurname",
                table: "Testimonials",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
