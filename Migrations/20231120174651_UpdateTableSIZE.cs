using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableSIZE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHITIETSANPHAMs_SIZE_MASIZE",
                table: "CHITIETSANPHAMs");

            migrationBuilder.DropTable(
                name: "HINHANH_SANPHAMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SIZE",
                table: "SIZE");

            migrationBuilder.RenameTable(
                name: "SIZE",
                newName: "SIZEs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SIZEs",
                table: "SIZEs",
                column: "MASIZE");

            migrationBuilder.AddForeignKey(
                name: "FK_CHITIETSANPHAMs_SIZEs_MASIZE",
                table: "CHITIETSANPHAMs",
                column: "MASIZE",
                principalTable: "SIZEs",
                principalColumn: "MASIZE",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHITIETSANPHAMs_SIZEs_MASIZE",
                table: "CHITIETSANPHAMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SIZEs",
                table: "SIZEs");

            migrationBuilder.RenameTable(
                name: "SIZEs",
                newName: "SIZE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SIZE",
                table: "SIZE",
                column: "MASIZE");

            migrationBuilder.CreateTable(
                name: "HINHANH_SANPHAMs",
                columns: table => new
                {
                    MAHINHANH = table.Column<int>(type: "int", nullable: false),
                    MASP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HINHANH_SANPHAMs", x => new { x.MAHINHANH, x.MASP });
                    table.ForeignKey(
                        name: "FK_HINHANH_SANPHAMs_HINHANHs_MAHINHANH",
                        column: x => x.MAHINHANH,
                        principalTable: "HINHANHs",
                        principalColumn: "MAHINHANH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HINHANH_SANPHAMs_SANPHAMs_MASP",
                        column: x => x.MASP,
                        principalTable: "SANPHAMs",
                        principalColumn: "MASP",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HINHANH_SANPHAMs_MASP",
                table: "HINHANH_SANPHAMs",
                column: "MASP");

            migrationBuilder.AddForeignKey(
                name: "FK_CHITIETSANPHAMs_SIZE_MASIZE",
                table: "CHITIETSANPHAMs",
                column: "MASIZE",
                principalTable: "SIZE",
                principalColumn: "MASIZE",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
