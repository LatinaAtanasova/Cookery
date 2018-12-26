using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookery.Data.Data.Migrations
{
    public partial class booleanPropertyToRecipeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Recipes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Recipes");
        }
    }
}
