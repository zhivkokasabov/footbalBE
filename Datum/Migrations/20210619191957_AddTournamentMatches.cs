using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddTournamentMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatch_Tournaments_TournamentId",
                table: "TournamentMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTeam_TournamentMatch_TournamentMatchId",
                table: "TournamentMatchTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentMatch",
                table: "TournamentMatch");

            migrationBuilder.RenameTable(
                name: "TournamentMatch",
                newName: "TournamentMatches");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentMatch_TournamentId",
                table: "TournamentMatches",
                newName: "IX_TournamentMatches_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentMatches",
                table: "TournamentMatches",
                column: "TournamentMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatches_Tournaments_TournamentId",
                table: "TournamentMatches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTeam_TournamentMatches_TournamentMatchId",
                table: "TournamentMatchTeam",
                column: "TournamentMatchId",
                principalTable: "TournamentMatches",
                principalColumn: "TournamentMatchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatches_Tournaments_TournamentId",
                table: "TournamentMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTeam_TournamentMatches_TournamentMatchId",
                table: "TournamentMatchTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentMatches",
                table: "TournamentMatches");

            migrationBuilder.RenameTable(
                name: "TournamentMatches",
                newName: "TournamentMatch");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentMatches_TournamentId",
                table: "TournamentMatch",
                newName: "IX_TournamentMatch_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentMatch",
                table: "TournamentMatch",
                column: "TournamentMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatch_Tournaments_TournamentId",
                table: "TournamentMatch",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTeam_TournamentMatch_TournamentMatchId",
                table: "TournamentMatchTeam",
                column: "TournamentMatchId",
                principalTable: "TournamentMatch",
                principalColumn: "TournamentMatchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
