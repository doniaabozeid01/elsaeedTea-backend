using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class addOrderRequestTableAndConnectItWithCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderRequestId",
                table: "Cart",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderRequest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRequest", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_OrderRequestId",
                table: "Cart",
                column: "OrderRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_OrderRequest_OrderRequestId",
                table: "Cart",
                column: "OrderRequestId",
                principalTable: "OrderRequest",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_OrderRequest_OrderRequestId",
                table: "Cart");

            migrationBuilder.DropTable(
                name: "OrderRequest");

            migrationBuilder.DropIndex(
                name: "IX_Cart_OrderRequestId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "OrderRequestId",
                table: "Cart");
        }
    }
}
