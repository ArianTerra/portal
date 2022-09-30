using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DataAccess.Migrations
{
    public partial class RenameArticleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Materials",
                newName: "PublishDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Materials",
                newName: "Date");
        }
    }
}
