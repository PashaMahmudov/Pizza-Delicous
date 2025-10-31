using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizza.Migrations
{
    /// <inheritdoc />
    public partial class AddColumHomeSliderToStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sliders");
        }
    }
}
