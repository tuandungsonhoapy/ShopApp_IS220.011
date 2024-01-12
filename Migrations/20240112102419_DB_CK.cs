using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnTapCK.Migrations
{
    /// <inheritdoc />
    public partial class DB_CK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HANHKHACH",
                columns: table => new
                {
                    MAKH = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HOTEN = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DIACHI = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DIENTHOAI = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HANHKHACH", x => x.MAKH);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MAYBAY",
                columns: table => new
                {
                    MAMB = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HANGMB = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SOCHO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAYBAY", x => x.MAMB);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CHUYENBAY",
                columns: table => new
                {
                    MACH = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CHUYEN = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DDI = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DDEN = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NGAY = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GBAY = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GDEN = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    THUONG = table.Column<int>(type: "int", nullable: false),
                    VIP = table.Column<int>(type: "int", nullable: false),
                    MAMB = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHUYENBAY", x => x.MACH);
                    table.ForeignKey(
                        name: "FK_CHUYENBAY_MAYBAY_MAMB",
                        column: x => x.MAMB,
                        principalTable: "MAYBAY",
                        principalColumn: "MAMB",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CT_CB",
                columns: table => new
                {
                    MACH = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MAHK = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SOGHE = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LOAIGHE = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_CB", x => new { x.MACH, x.MAHK });
                    table.ForeignKey(
                        name: "FK_CT_CB_CHUYENBAY_MACH",
                        column: x => x.MACH,
                        principalTable: "CHUYENBAY",
                        principalColumn: "MACH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_CB_HANHKHACH_MAHK",
                        column: x => x.MAHK,
                        principalTable: "HANHKHACH",
                        principalColumn: "MAKH",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CHUYENBAY_MAMB",
                table: "CHUYENBAY",
                column: "MAMB");

            migrationBuilder.CreateIndex(
                name: "IX_CT_CB_MAHK",
                table: "CT_CB",
                column: "MAHK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_CB");

            migrationBuilder.DropTable(
                name: "CHUYENBAY");

            migrationBuilder.DropTable(
                name: "HANHKHACH");

            migrationBuilder.DropTable(
                name: "MAYBAY");
        }
    }
}
