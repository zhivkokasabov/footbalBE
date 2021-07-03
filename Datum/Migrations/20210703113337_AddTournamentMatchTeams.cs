using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddTournamentMatchTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTeam_Teams_TeamId",
                table: "TournamentMatchTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTeam_TournamentMatches_TournamentMatchId",
                table: "TournamentMatchTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentMatchTeam",
                table: "TournamentMatchTeam");

            migrationBuilder.RenameTable(
                name: "TournamentMatchTeam",
                newName: "TournamentMatchTeams");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentMatchTeam_TournamentMatchId",
                table: "TournamentMatchTeams",
                newName: "IX_TournamentMatchTeams_TournamentMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentMatchTeam_TeamId",
                table: "TournamentMatchTeams",
                newName: "IX_TournamentMatchTeams_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentMatchTeams",
                table: "TournamentMatchTeams",
                column: "TournamentMatchTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTeams_Teams_TeamId",
                table: "TournamentMatchTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTeams_TournamentMatches_TournamentMatchId",
                table: "TournamentMatchTeams",
                column: "TournamentMatchId",
                principalTable: "TournamentMatches",
                principalColumn: "TournamentMatchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTeams_Teams_TeamId",
                table: "TournamentMatchTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTeams_TournamentMatches_TournamentMatchId",
                table: "TournamentMatchTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentMatchTeams",
                table: "TournamentMatchTeams");

            migrationBuilder.RenameTable(
                name: "TournamentMatchTeams",
                newName: "TournamentMatchTeam");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentMatchTeams_TournamentMatchId",
                table: "TournamentMatchTeam",
                newName: "IX_TournamentMatchTeam_TournamentMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentMatchTeams_TeamId",
                table: "TournamentMatchTeam",
                newName: "IX_TournamentMatchTeam_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentMatchTeam",
                table: "TournamentMatchTeam",
                column: "TournamentMatchTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTeam_Teams_TeamId",
                table: "TournamentMatchTeam",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTeam_TournamentMatches_TournamentMatchId",
                table: "TournamentMatchTeam",
                column: "TournamentMatchId",
                principalTable: "TournamentMatches",
                principalColumn: "TournamentMatchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
