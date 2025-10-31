using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizza.Migrations
{
    /// <inheritdoc />
    public partial class AddFormfileandIsactiveColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OurServis",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OurServis");
        }
    }
}
