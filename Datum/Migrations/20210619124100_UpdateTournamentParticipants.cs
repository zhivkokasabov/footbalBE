using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class UpdateTournamentParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Team_TeamId",
                table: "TournamentParticipant");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TournamentParticipant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Team_TeamId",
                table: "TournamentParticipant",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Team_TeamId",
                table: "TournamentParticipant");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TournamentParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Team_TeamId",
                table: "TournamentParticipant",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
