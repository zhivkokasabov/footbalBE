using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Team_TeamId",
                table: "TournamentParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Tournaments_TournamentId",
                table: "TournamentParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentParticipant",
                table: "TournamentParticipant");

            migrationBuilder.RenameTable(
                name: "TournamentParticipant",
                newName: "TournamentParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipant_TournamentId",
                table: "TournamentParticipants",
                newName: "IX_TournamentParticipants_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipant_TeamId",
                table: "TournamentParticipants",
                newName: "IX_TournamentParticipants_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentParticipants",
                table: "TournamentParticipants",
                column: "TournamentParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipants_Team_TeamId",
                table: "TournamentParticipants",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipants_Tournaments_TournamentId",
                table: "TournamentParticipants",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipants_Team_TeamId",
                table: "TournamentParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipants_Tournaments_TournamentId",
                table: "TournamentParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentParticipants",
                table: "TournamentParticipants");

            migrationBuilder.RenameTable(
                name: "TournamentParticipants",
                newName: "TournamentParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipants_TournamentId",
                table: "TournamentParticipant",
                newName: "IX_TournamentParticipant_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipants_TeamId",
                table: "TournamentParticipant",
                newName: "IX_TournamentParticipant_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentParticipant",
                table: "TournamentParticipant",
                column: "TournamentParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Team_TeamId",
                table: "TournamentParticipant",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Tournaments_TournamentId",
                table: "TournamentParticipant",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
