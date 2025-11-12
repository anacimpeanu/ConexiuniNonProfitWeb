using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexiuniNonProfit.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileCategory",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileCategory",
                table: "Profiles");
        }
    }
}
