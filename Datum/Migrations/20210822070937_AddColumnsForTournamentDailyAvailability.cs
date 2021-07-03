using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddColumnsForTournamentDailyAvailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FirstMatchStartAt",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMatchStartsAt",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstMatchStartAt",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "LastMatchStartsAt",
                table: "Tournaments");
        }
    }
}
