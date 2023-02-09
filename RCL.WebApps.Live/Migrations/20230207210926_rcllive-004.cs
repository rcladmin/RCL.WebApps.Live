using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCL.WebApps.Live.Migrations
{
    /// <inheritdoc />
    public partial class rcllive004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rcl_app_live_event_attendee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    eventId = table.Column<int>(type: "int", nullable: false),
                    dateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    orderId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rcl_app_live_event_attendee", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rcl_app_live_event_attendee");
        }
    }
}
