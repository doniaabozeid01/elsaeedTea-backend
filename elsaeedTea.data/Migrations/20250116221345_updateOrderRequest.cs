using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class updateOrderRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_OrderRequest_OrderRequestId",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderRequest",
                table: "OrderRequest");

            migrationBuilder.RenameTable(
                name: "OrderRequest",
                newName: "orderRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderRequest",
                table: "orderRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_orderRequest_OrderRequestId",
                table: "Cart",
                column: "OrderRequestId",
                principalTable: "orderRequest",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_orderRequest_OrderRequestId",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderRequest",
                table: "orderRequest");

            migrationBuilder.RenameTable(
                name: "orderRequest",
                newName: "OrderRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderRequest",
                table: "OrderRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_OrderRequest_OrderRequestId",
                table: "Cart",
                column: "OrderRequestId",
                principalTable: "OrderRequest",
                principalColumn: "Id");
        }
    }
}
