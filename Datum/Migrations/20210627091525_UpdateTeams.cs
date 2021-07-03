using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class UpdateTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryKey",
                table: "Team",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTeamCaptain",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryKey",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "IsTeamCaptain",
                table: "AspNetUsers");
        }
    }
}
