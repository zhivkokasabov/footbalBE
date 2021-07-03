using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayingDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayingDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentAccesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Avenue = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    TeamsCount = table.Column<int>(nullable: false),
                    TeamsAdvancingAfterGroups = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Rules = table.Column<string>(maxLength: 2000, nullable: true),
                    PlayingFieldsCount = table.Column<int>(nullable: false),
                    MatchLength = table.Column<TimeSpan>(nullable: false),
                    HalfTimeLength = table.Column<TimeSpan>(nullable: false),
                    GroupSize = table.Column<int>(nullable: false),
                    FirstMatchStartsAt = table.Column<TimeSpan>(nullable: false),
                    PlayingDaysId = table.Column<int>(nullable: false),
                    TournamentAccessId = table.Column<int>(nullable: false),
                    TournamentTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_PlayingDays_PlayingDaysId",
                        column: x => x.PlayingDaysId,
                        principalTable: "PlayingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentAccesses_TournamentAccessId",
                        column: x => x.TournamentAccessId,
                        principalTable: "TournamentAccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentTypes_TournamentTypeId",
                        column: x => x.TournamentTypeId,
                        principalTable: "TournamentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_PlayingDaysId",
                table: "Tournaments",
                column: "PlayingDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentAccessId",
                table: "Tournaments",
                column: "TournamentAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentTypeId",
                table: "Tournaments",
                column: "TournamentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_UserId",
                table: "Tournaments",
                column: "UserId");

            migrationBuilder.Sql("INSERT INTO TournamentTypes (Name) Values ('elimination tournament')");
            migrationBuilder.Sql("INSERT INTO TournamentTypes (Name) Values ('classic tournament')");
            migrationBuilder.Sql("INSERT INTO TournamentTypes (Name) Values ('round-robin tournament')");
            migrationBuilder.Sql("INSERT INTO TournamentTypes (Name) Values ('double round-robin tournament')");

            migrationBuilder.Sql("INSERT INTO TournamentAccesses (Name) Values ('public')");
            migrationBuilder.Sql("INSERT INTO TournamentAccesses (Name) Values ('private')");
            migrationBuilder.Sql("INSERT INTO TournamentAccesses (Name) Values ('request access')");

            migrationBuilder.Sql("INSERT INTO PlayingDays (Name) Values ('work days')");
            migrationBuilder.Sql("INSERT INTO PlayingDays (Name) Values ('week end')");
            migrationBuilder.Sql("INSERT INTO PlayingDays (Name) Values ('whole week')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "PlayingDays");

            migrationBuilder.DropTable(
                name: "TournamentAccesses");

            migrationBuilder.DropTable(
                name: "TournamentTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
