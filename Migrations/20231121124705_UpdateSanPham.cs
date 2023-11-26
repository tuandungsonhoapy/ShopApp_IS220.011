using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSanPham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PLTHOITRANG",
                table: "SANPHAMs",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PLTHOITRANG",
                table: "SANPHAMs");
        }
    }
}
