using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizza.Migrations
{
    /// <inheritdoc />
    public partial class AddImgToOurService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "OurServis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "OurServis");
        }
    }
}
