using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCL.WebApps.Live.Migrations
{
    /// <inheritdoc />
    public partial class rcllive002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "presenterId",
                table: "rcl_app_live_event",
                newName: "presenterEmail");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "rcl_app_live_event",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "rcl_app_live_event",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                table: "rcl_app_live_event");

            migrationBuilder.DropColumn(
                name: "price",
                table: "rcl_app_live_event");

            migrationBuilder.RenameColumn(
                name: "presenterEmail",
                table: "rcl_app_live_event",
                newName: "presenterId");
        }
    }
}
