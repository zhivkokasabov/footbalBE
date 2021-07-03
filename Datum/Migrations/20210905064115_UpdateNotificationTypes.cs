using Microsoft.EntityFrameworkCore.Migrations;

namespace Datum.Migrations
{
    public partial class UpdateNotificationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 9, "alert" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Notifications");
        }
    }
}
