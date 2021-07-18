using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddFilterToUserPositionsIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPositions",
                table: "UserPositions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserPositions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPositions",
                table: "UserPositions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPositions_UserId_PlayerPositionId",
                table: "UserPositions",
                columns: new[] { "UserId", "PlayerPositionId" },
                filter: "Active = true");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPositions",
                table: "UserPositions");

            migrationBuilder.DropIndex(
                name: "IX_UserPositions_UserId_PlayerPositionId",
                table: "UserPositions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserPositions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPositions",
                table: "UserPositions",
                columns: new[] { "UserId", "PlayerPositionId" });
        }
    }
}
