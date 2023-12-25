using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class updateVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GIADONTOITHIEU",
                table: "VOUCHERs",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GIAMTOIDA",
                table: "VOUCHERs",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "LOAIVOUCHER",
                table: "VOUCHERs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GIADONTOITHIEU",
                table: "VOUCHERs");

            migrationBuilder.DropColumn(
                name: "GIAMTOIDA",
                table: "VOUCHERs");

            migrationBuilder.DropColumn(
                name: "LOAIVOUCHER",
                table: "VOUCHERs");
        }
    }
}
