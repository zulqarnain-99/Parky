using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkyAPI.Migrations
{
    public partial class addTrailsTodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NationalPark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Established = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalPark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Distance = table.Column<string>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false),
                    NationalParkId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trail_NationalPark_NationalParkId",
                        column: x => x.NationalParkId,
                        principalTable: "NationalPark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trail_NationalParkId",
                table: "Trail",
                column: "NationalParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trail");

            migrationBuilder.DropTable(
                name: "NationalPark");
        }
    }
}
