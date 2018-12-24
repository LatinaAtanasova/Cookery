using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookery.Data.Data.Migrations
{
    public partial class AddedPropertyToShoppingItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "ShoppingItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "ShoppingItems");
        }
    }
}
