using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class AddStatusColumnToOrderRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "orderRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "orderRequest");
        }
    }
}
