using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectForRoleAndOutcome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyRole",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyRole",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "Projects");
        }
    }
}
