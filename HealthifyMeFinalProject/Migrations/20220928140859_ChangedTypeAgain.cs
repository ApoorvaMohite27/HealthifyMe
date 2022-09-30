using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthifyMeFinalProject.Migrations
{
    public partial class ChangedTypeAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "FeedBackForms",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "FeedBackForms",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
