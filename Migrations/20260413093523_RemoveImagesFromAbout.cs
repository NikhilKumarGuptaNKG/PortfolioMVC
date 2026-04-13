using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioMVC.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImagesFromAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Abouts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Abouts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Abouts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
