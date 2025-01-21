using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class updateTeaProductAndAddTeaProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TeaProduct");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TeaProduct");

            migrationBuilder.CreateTable(
                name: "TeaProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeaProductDetails_TeaProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TeaProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeaProductDetails_ProductId",
                table: "TeaProductDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeaProductDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TeaProduct",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "TeaProduct",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
