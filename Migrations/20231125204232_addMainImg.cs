using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class addMainImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImg",
                table: "SANPHAMs",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImg",
                table: "SANPHAMs");
        }
    }
}
