using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DONHANGs_THONGTINGIAOHANGs_MATTGH",
                table: "DONHANGs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NGAYGIAO",
                table: "DONHANGs",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "MATTGH",
                table: "DONHANGs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "NGAYSUADOI",
                table: "DONHANGs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANGs_THONGTINGIAOHANGs_MATTGH",
                table: "DONHANGs",
                column: "MATTGH",
                principalTable: "THONGTINGIAOHANGs",
                principalColumn: "MATTGH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DONHANGs_THONGTINGIAOHANGs_MATTGH",
                table: "DONHANGs");

            migrationBuilder.DropColumn(
                name: "NGAYSUADOI",
                table: "DONHANGs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NGAYGIAO",
                table: "DONHANGs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MATTGH",
                table: "DONHANGs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANGs_THONGTINGIAOHANGs_MATTGH",
                table: "DONHANGs",
                column: "MATTGH",
                principalTable: "THONGTINGIAOHANGs",
                principalColumn: "MATTGH",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
