using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthifyMeFinalProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 450, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    CurrentWeight = table.Column<decimal>(nullable: false),
                    TargetWeight = table.Column<decimal>(nullable: false),
                    MedicalCondition = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Dietitians",
                columns: table => new
                {
                    DietitianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietitianName = table.Column<string>(nullable: false),
                    Specialist = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DietitianPhoto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dietitians", x => x.DietitianId);
                });

            migrationBuilder.CreateTable(
                name: "AssignTasks",
                columns: table => new
                {
                    AssignTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diet = table.Column<string>(maxLength: 1000, nullable: false),
                    Exercise = table.Column<string>(maxLength: 1000, nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    DietitianId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignTasks", x => x.AssignTaskId);
                    table.ForeignKey(
                        name: "FK_AssignTasks_CustomerDetails_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDetails",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignTasks_Dietitians_DietitianId",
                        column: x => x.DietitianId,
                        principalTable: "Dietitians",
                        principalColumn: "DietitianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignTasks_CustomerId",
                table: "AssignTasks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignTasks_DietitianId",
                table: "AssignTasks",
                column: "DietitianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignTasks");

            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropTable(
                name: "Dietitians");
        }
    }
}
