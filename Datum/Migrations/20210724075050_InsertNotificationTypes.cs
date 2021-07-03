using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class InsertNotificationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationType_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPositions_PlayerPosition_PlayerPositionId",
                table: "UserPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerPosition",
                table: "PlayerPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationType",
                table: "NotificationType");

            migrationBuilder.RenameTable(
                name: "PlayerPosition",
                newName: "PlayerPositions");

            migrationBuilder.RenameTable(
                name: "NotificationType",
                newName: "NotificationTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTypes",
                table: "NotificationTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPositions_PlayerPositions_PlayerPositionId",
                table: "UserPositions",
                column: "PlayerPositionId",
                principalTable: "PlayerPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "join team" },
                    { 2, "join tournament" },
                    { 3, "challenge team" },
                    { 4, "leave team" },
                    { 5, "request to join team" },
                    { 6, "request to join tournament" },
                    { 7, "edit tournament matches" },
                    { 8, "leave tournament" },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPositions_PlayerPositions_PlayerPositionId",
                table: "UserPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTypes",
                table: "NotificationTypes");

            migrationBuilder.RenameTable(
                name: "PlayerPositions",
                newName: "PlayerPosition");

            migrationBuilder.RenameTable(
                name: "NotificationTypes",
                newName: "NotificationType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerPosition",
                table: "PlayerPosition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationType",
                table: "NotificationType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationType_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId",
                principalTable: "NotificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPositions_PlayerPosition_PlayerPositionId",
                table: "UserPositions",
                column: "PlayerPositionId",
                principalTable: "PlayerPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8
                }
            );
        }
    }
}
