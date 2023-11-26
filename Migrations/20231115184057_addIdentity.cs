using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAFW_IS220.Migrations
{
    /// <inheritdoc />
    public partial class addIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HINHANHs",
                columns: table => new
                {
                    MAHINHANH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LINK = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HINHANHs", x => x.MAHINHANH);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MAUSACs",
                columns: table => new
                {
                    MAMAU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TENMAU = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HEX = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAUSACs", x => x.MAMAU);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PL_SPs",
                columns: table => new
                {
                    MAPL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TENPL = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PL_SPs", x => x.MAPL);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TENKH = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GIOITINH = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NGAYDK = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VOUCHERs",
                columns: table => new
                {
                    MAVOUCHER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TENVOUCHER = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    THOIGIANBD = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    THOIGIANKT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MOTA = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GIATRIGIAM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOUCHERs", x => x.MAVOUCHER);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SANPHAMs",
                columns: table => new
                {
                    MASP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TENSP = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GIAGOC = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MAPL = table.Column<int>(type: "int", nullable: false),
                    MOTA = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GIABAN = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SANPHAMs", x => x.MASP);
                    table.ForeignKey(
                        name: "FK_SANPHAMs_PL_SPs_MAPL",
                        column: x => x.MAPL,
                        principalTable: "PL_SPs",
                        principalColumn: "MAPL",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "THONGTINGIAOHANGs",
                columns: table => new
                {
                    MATTGH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MATK = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DIACHI = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SDT = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NGAYTAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NGAYHETDUNG = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THONGTINGIAOHANGs", x => x.MATTGH);
                    table.ForeignKey(
                        name: "FK_THONGTINGIAOHANGs_Users_MATK",
                        column: x => x.MATK,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CHITIETSANPHAMs",
                columns: table => new
                {
                    MACTSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MAMAU = table.Column<int>(type: "int", nullable: false),
                    SIZE = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MASP = table.Column<int>(type: "int", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETSANPHAMs", x => x.MACTSP);
                    table.ForeignKey(
                        name: "FK_CHITIETSANPHAMs_MAUSACs_MAMAU",
                        column: x => x.MAMAU,
                        principalTable: "MAUSACs",
                        principalColumn: "MAMAU",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETSANPHAMs_SANPHAMs_MASP",
                        column: x => x.MASP,
                        principalTable: "SANPHAMs",
                        principalColumn: "MASP",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DANHGIAs",
                columns: table => new
                {
                    MADANHGIA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MATK = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MASP = table.Column<int>(type: "int", nullable: false),
                    SOSAO = table.Column<int>(type: "int", nullable: false),
                    NOIDUNG = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANHGIAs", x => x.MADANHGIA);
                    table.ForeignKey(
                        name: "FK_DANHGIAs_SANPHAMs_MASP",
                        column: x => x.MASP,
                        principalTable: "SANPHAMs",
                        principalColumn: "MASP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DANHGIAs_Users_MATK",
                        column: x => x.MATK,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "DONHANGs",
                columns: table => new
                {
                    MADH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MATK = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NGAYMUA = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TONGTIEN = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NGAYGIAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HINHTHUCTHANHTOAN = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRANGTHAITHANHTOAN = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRANGTHAIDONHANG = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MATTGH = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DONHANGs", x => x.MADH);
                    table.ForeignKey(
                        name: "FK_DONHANGs_THONGTINGIAOHANGs_MATTGH",
                        column: x => x.MATTGH,
                        principalTable: "THONGTINGIAOHANGs",
                        principalColumn: "MATTGH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DONHANGs_Users_MATK",
                        column: x => x.MATK,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CHITIETGIOHANGs",
                columns: table => new
                {
                    MATK = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MASP = table.Column<int>(type: "int", nullable: false),
                    MACTSP = table.Column<int>(type: "int", nullable: false),
                    SOLUONGMUA = table.Column<int>(type: "int", nullable: false),
                    TONGGIA = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETGIOHANGs", x => new { x.MATK, x.MASP, x.MACTSP });
                    table.ForeignKey(
                        name: "FK_CHITIETGIOHANGs_CHITIETSANPHAMs_MACTSP",
                        column: x => x.MACTSP,
                        principalTable: "CHITIETSANPHAMs",
                        principalColumn: "MACTSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETGIOHANGs_SANPHAMs_MASP",
                        column: x => x.MASP,
                        principalTable: "SANPHAMs",
                        principalColumn: "MASP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETGIOHANGs_Users_MATK",
                        column: x => x.MATK,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CTDHs",
                columns: table => new
                {
                    MADH = table.Column<int>(type: "int", nullable: false),
                    MACTSP = table.Column<int>(type: "int", nullable: false),
                    TONGGIA = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTDHs", x => new { x.MADH, x.MACTSP });
                    table.ForeignKey(
                        name: "FK_CTDHs_CHITIETSANPHAMs_MACTSP",
                        column: x => x.MACTSP,
                        principalTable: "CHITIETSANPHAMs",
                        principalColumn: "MACTSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTDHs_DONHANGs_MADH",
                        column: x => x.MADH,
                        principalTable: "DONHANGs",
                        principalColumn: "MADH",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "THANHTOANs",
                columns: table => new
                {
                    MATHANHTOAN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MADH = table.Column<int>(type: "int", nullable: false),
                    SOTIEN = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NGAYTHANHTOAN = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THANHTOANs", x => x.MATHANHTOAN);
                    table.ForeignKey(
                        name: "FK_THANHTOANs_DONHANGs_MADH",
                        column: x => x.MADH,
                        principalTable: "DONHANGs",
                        principalColumn: "MADH",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VOUCHER_DONHANGs",
                columns: table => new
                {
                    MAVOUCHER = table.Column<int>(type: "int", nullable: false),
                    MADH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOUCHER_DONHANGs", x => new { x.MADH, x.MAVOUCHER });
                    table.ForeignKey(
                        name: "FK_VOUCHER_DONHANGs_DONHANGs_MADH",
                        column: x => x.MADH,
                        principalTable: "DONHANGs",
                        principalColumn: "MADH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VOUCHER_DONHANGs_VOUCHERs_MAVOUCHER",
                        column: x => x.MAVOUCHER,
                        principalTable: "VOUCHERs",
                        principalColumn: "MAVOUCHER",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "index-giohang-tonggia",
                table: "CHITIETGIOHANGs",
                column: "TONGGIA");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETGIOHANGs_MACTSP",
                table: "CHITIETGIOHANGs",
                column: "MACTSP");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETGIOHANGs_MASP",
                table: "CHITIETGIOHANGs",
                column: "MASP");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETSANPHAMs_MAMAU",
                table: "CHITIETSANPHAMs",
                column: "MAMAU");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETSANPHAMs_MASP",
                table: "CHITIETSANPHAMs",
                column: "MASP");

            migrationBuilder.CreateIndex(
                name: "index-CTDH-tonggia",
                table: "CTDHs",
                column: "TONGGIA");

            migrationBuilder.CreateIndex(
                name: "IX_CTDHs_MACTSP",
                table: "CTDHs",
                column: "MACTSP");

            migrationBuilder.CreateIndex(
                name: "IX_DANHGIAs_MASP",
                table: "DANHGIAs",
                column: "MASP");

            migrationBuilder.CreateIndex(
                name: "IX_DANHGIAs_MATK",
                table: "DANHGIAs",
                column: "MATK");

            migrationBuilder.CreateIndex(
                name: "index-DONHANG-tongtien",
                table: "DONHANGs",
                column: "TONGTIEN");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANGs_MATK",
                table: "DONHANGs",
                column: "MATK");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANGs_MATTGH",
                table: "DONHANGs",
                column: "MATTGH");

            migrationBuilder.CreateIndex(
                name: "IX_HINHANH_SANPHAMs_MASP",
                table: "HINHANH_SANPHAMs",
                column: "MASP");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index-SANPHAM-giaban",
                table: "SANPHAMs",
                column: "GIABAN");

            migrationBuilder.CreateIndex(
                name: "index-SANPHAM-giagoc",
                table: "SANPHAMs",
                column: "GIAGOC");

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAMs_MAPL",
                table: "SANPHAMs",
                column: "MAPL");

            migrationBuilder.CreateIndex(
                name: "index-THANHTOAN-sotien",
                table: "THANHTOANs",
                column: "SOTIEN");

            migrationBuilder.CreateIndex(
                name: "IX_THANHTOANs_MADH",
                table: "THANHTOANs",
                column: "MADH");

            migrationBuilder.CreateIndex(
                name: "IX_THONGTINGIAOHANGs_MATK",
                table: "THONGTINGIAOHANGs",
                column: "MATK");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VOUCHER_DONHANGs_MAVOUCHER",
                table: "VOUCHER_DONHANGs",
                column: "MAVOUCHER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHITIETGIOHANGs");

            migrationBuilder.DropTable(
                name: "CTDHs");

            migrationBuilder.DropTable(
                name: "DANHGIAs");

            migrationBuilder.DropTable(
                name: "HINHANH_SANPHAMs");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "THANHTOANs");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "VOUCHER_DONHANGs");

            migrationBuilder.DropTable(
                name: "CHITIETSANPHAMs");

            migrationBuilder.DropTable(
                name: "HINHANHs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DONHANGs");

            migrationBuilder.DropTable(
                name: "VOUCHERs");

            migrationBuilder.DropTable(
                name: "MAUSACs");

            migrationBuilder.DropTable(
                name: "SANPHAMs");

            migrationBuilder.DropTable(
                name: "THONGTINGIAOHANGs");

            migrationBuilder.DropTable(
                name: "PL_SPs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
