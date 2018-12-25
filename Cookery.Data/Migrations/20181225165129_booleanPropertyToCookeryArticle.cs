using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookery.Data.Data.Migrations
{
    public partial class booleanPropertyToCookeryArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "CookeryArticles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "CookeryArticles");
        }
    }
}
