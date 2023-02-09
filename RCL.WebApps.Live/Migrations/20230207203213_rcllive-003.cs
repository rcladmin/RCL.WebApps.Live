using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCL.WebApps.Live.Migrations
{
    /// <inheritdoc />
    public partial class rcllive003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rcl_app_live_transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    orderId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    chargeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    chargeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    chargeAmount = table.Column<int>(type: "int", nullable: false),
                    chargeAmountCurrency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    chargeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    chargeEmail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    chargeCountry = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    refundId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    refundDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    refundAmount = table.Column<int>(type: "int", nullable: false),
                    refundAmountCurrency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    refundReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    disputeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    disputeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    disputeAmount = table.Column<int>(type: "int", nullable: false),
                    disputeAmountCurrency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    disputeReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rcl_app_live_transaction", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rcl_app_live_transaction");
        }
    }
}
