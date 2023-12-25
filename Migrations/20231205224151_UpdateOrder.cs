using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MADH",
                table: "DANHGIAs",
                type: "int",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_DANHGIAs_DONHANGs_MADH",
                table: "DANHGIAs",
                column: "MADH",
                principalTable: "DONHANGs",
                principalColumn: "MADH",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DANHGIAs_DONHANGs_MADH",
                table: "DANHGIAs");

            migrationBuilder.DropIndex(
                name: "IX_DANHGIAs_MADH",
                table: "DANHGIAs");

            migrationBuilder.DropColumn(
                name: "MADH",
                table: "DANHGIAs");
        }
    }
}
