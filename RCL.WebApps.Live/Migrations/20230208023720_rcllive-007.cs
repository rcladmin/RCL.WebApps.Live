using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCL.WebApps.Live.Migrations
{
    /// <inheritdoc />
    public partial class rcllive007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateRegistered",
                table: "rcl_app_live_event_attendee",
                newName: "dateCreated");

            migrationBuilder.AddColumn<DateTime>(
                name: "chargeDate",
                table: "rcl_app_live_event_attendee",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "transactionId",
                table: "rcl_app_live_event_attendee",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chargeDate",
                table: "rcl_app_live_event_attendee");

            migrationBuilder.DropColumn(
                name: "transactionId",
                table: "rcl_app_live_event_attendee");

            migrationBuilder.RenameColumn(
                name: "dateCreated",
                table: "rcl_app_live_event_attendee",
                newName: "dateRegistered");
        }
    }
}
