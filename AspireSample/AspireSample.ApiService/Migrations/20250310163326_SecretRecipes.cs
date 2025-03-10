using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireSample.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class SecretRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSecret",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSecret",
                table: "Recipes");
        }
    }
}
