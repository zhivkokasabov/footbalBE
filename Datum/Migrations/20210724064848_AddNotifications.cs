using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class AddNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPositions_PlayerPositions_PlayerPositionId",
                table: "UserPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions");

            migrationBuilder.RenameTable(
                name: "PlayerPositions",
                newName: "PlayerPosition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerPosition",
                table: "PlayerPosition",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Pending = table.Column<bool>(type: "bit", nullable: false),
                    Rejected = table.Column<bool>(type: "bit", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPositions_PlayerPosition_PlayerPositionId",
                table: "UserPositions",
                column: "PlayerPositionId",
                principalTable: "PlayerPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPositions_PlayerPosition_PlayerPositionId",
                table: "UserPositions");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerPosition",
                table: "PlayerPosition");

            migrationBuilder.RenameTable(
                name: "PlayerPosition",
                newName: "PlayerPositions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPositions_PlayerPositions_PlayerPositionId",
                table: "UserPositions",
                column: "PlayerPositionId",
                principalTable: "PlayerPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
