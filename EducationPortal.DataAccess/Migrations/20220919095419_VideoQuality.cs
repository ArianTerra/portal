using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DataAccess.Migrations
{
    public partial class VideoQuality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Materials");

            migrationBuilder.AddColumn<Guid>(
                name: "QualityId",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VideoQualities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoQualities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_QualityId",
                table: "Materials",
                column: "QualityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_VideoQualities_QualityId",
                table: "Materials",
                column: "QualityId",
                principalTable: "VideoQualities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_VideoQualities_QualityId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "VideoQualities");

            migrationBuilder.DropIndex(
                name: "IX_Materials_QualityId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "QualityId",
                table: "Materials");

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
