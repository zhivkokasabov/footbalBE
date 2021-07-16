using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddScoreColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConceivedGoals",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Draws",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalDifference",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Goals",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Loses",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Played",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "TournamentParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "TournamentMatches",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConceivedGoals",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Draws",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "GoalDifference",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Loses",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Played",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "TournamentParticipants");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "TournamentMatches");
        }
    }
}
