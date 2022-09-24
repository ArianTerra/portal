using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DataAccess.Migrations
{
    public partial class DeleteSkillDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Skills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
