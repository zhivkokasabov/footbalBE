using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddIsEliminationToMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEliminationMatch",
                table: "TournamentMatches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEliminationMatch",
                table: "TournamentMatches");
        }
    }
}
