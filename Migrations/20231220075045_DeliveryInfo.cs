using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "THONGTINVANCHUYENs",
                columns: table => new
                {
                    MADH = table.Column<int>(type: "int", nullable: false),
                    MATTGH = table.Column<int>(type: "int", nullable: false),
                    NGAYGIAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NGAYKETTHUC = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THONGTINVANCHUYENs", x => new { x.MADH, x.MATTGH });
                    table.ForeignKey(
                        name: "FK_THONGTINVANCHUYENs_DONHANGs_MADH",
                        column: x => x.MADH,
                        principalTable: "DONHANGs",
                        principalColumn: "MADH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_THONGTINVANCHUYENs_THONGTINGIAOHANGs_MATTGH",
                        column: x => x.MATTGH,
                        principalTable: "THONGTINGIAOHANGs",
                        principalColumn: "MATTGH",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_THONGTINVANCHUYENs_MATTGH",
                table: "THONGTINVANCHUYENs",
                column: "MATTGH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "THONGTINVANCHUYENs");
        }
    }
}
