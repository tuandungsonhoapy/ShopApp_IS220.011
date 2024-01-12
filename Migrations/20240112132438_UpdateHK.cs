using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnTapCK.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MAKH",
                table: "HANHKHACH",
                newName: "MAHK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MAHK",
                table: "HANHKHACH",
                newName: "MAKH");
        }
    }
}
