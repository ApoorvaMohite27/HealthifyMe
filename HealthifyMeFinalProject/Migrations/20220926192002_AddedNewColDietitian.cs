using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthifyMeFinalProject.Migrations
{
    public partial class AddedNewColDietitian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DietitianUserName",
                table: "Dietitians",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DietitianUserName",
                table: "Dietitians");
        }
    }
}
