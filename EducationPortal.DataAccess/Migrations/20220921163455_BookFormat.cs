using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DataAccess.Migrations
{
    public partial class BookFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Format",
                table: "Materials");

            migrationBuilder.AddColumn<Guid>(
                name: "BookFormatId",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookFormats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookFormats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_BookFormatId",
                table: "Materials",
                column: "BookFormatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_BookFormats_BookFormatId",
                table: "Materials",
                column: "BookFormatId",
                principalTable: "BookFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_BookFormats_BookFormatId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "BookFormats");

            migrationBuilder.DropIndex(
                name: "IX_Materials_BookFormatId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "BookFormatId",
                table: "Materials");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
