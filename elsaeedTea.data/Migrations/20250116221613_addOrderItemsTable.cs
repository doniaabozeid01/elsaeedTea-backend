using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class addOrderItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderRequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderItems_orderRequest_OrderRequestId",
                        column: x => x.OrderRequestId,
                        principalTable: "orderRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderItems_TeaProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TeaProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderRequestId",
                table: "orderItems",
                column: "OrderRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_ProductId",
                table: "orderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_UserId",
                table: "orderItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");
        }
    }
}
