using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class CloseTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasFinished",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TournamentPlacements",
                columns: table => new
                {
                    TournamentPlacementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Placement = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPlacements", x => x.TournamentPlacementId);
                    table.ForeignKey(
                        name: "FK_TournamentPlacements_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentPlacements_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlacements_TeamId",
                table: "TournamentPlacements",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlacements_TournamentId",
                table: "TournamentPlacements",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentPlacements");

            migrationBuilder.DropColumn(
                name: "HasFinished",
                table: "Tournaments");
        }
    }
}
