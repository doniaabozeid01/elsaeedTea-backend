using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class updateRelationFromProductToProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_TeaProduct_ProductId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_TeaProduct_ProductId",
                table: "orderItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "orderItems",
                newName: "ProductDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_orderItems_ProductId",
                table: "orderItems",
                newName: "IX_orderItems_ProductDetailsId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Cart",
                newName: "ProductDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                newName: "IX_Cart_ProductDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_TeaProductDetails_ProductDetailsId",
                table: "Cart",
                column: "ProductDetailsId",
                principalTable: "TeaProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_TeaProductDetails_ProductDetailsId",
                table: "orderItems",
                column: "ProductDetailsId",
                principalTable: "TeaProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_TeaProductDetails_ProductDetailsId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_TeaProductDetails_ProductDetailsId",
                table: "orderItems");

            migrationBuilder.RenameColumn(
                name: "ProductDetailsId",
                table: "orderItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_orderItems_ProductDetailsId",
                table: "orderItems",
                newName: "IX_orderItems_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductDetailsId",
                table: "Cart",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_ProductDetailsId",
                table: "Cart",
                newName: "IX_Cart_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_TeaProduct_ProductId",
                table: "Cart",
                column: "ProductId",
                principalTable: "TeaProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_TeaProduct_ProductId",
                table: "orderItems",
                column: "ProductId",
                principalTable: "TeaProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
