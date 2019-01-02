using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookery.Data.Data.Migrations
{
    public partial class ShoppingItemQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingItems");
        }
    }
}
