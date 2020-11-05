using Microsoft.EntityFrameworkCore.Migrations;

namespace AnalysisWebApp.Migrations
{
    public partial class inhhss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasicOrganID",
                table: "UserChoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicOrganID",
                table: "UserChoices");
        }
    }
}
