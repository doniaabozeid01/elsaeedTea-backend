using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elsaeedTea.data.Migrations
{
    public partial class addAddressToOrderRequestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Governorate",
                table: "orderRequest",
                newName: "city");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "orderRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "orderRequest");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "orderRequest",
                newName: "Governorate");
        }
    }
}
