using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiemRenLuyenMVC.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    TenDangNhap = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.TenDangNhap);
                });

            migrationBuilder.InsertData(
                table: "TaiKhoan",
                columns: new[] { "TenDangNhap", "MatKhau", "VaiTro" },
                values: new object[,]
                {
                    { "22103100228", "123456", "SV" },
                    { "22103100229", "123456", "SV" },
                    { "22103100230", "123456", "SV" },
                    { "22103100231", "123456", "SV" },
                    { "22103100232", "123456", "SV" },
                    { "22103110301", "123456", "GV" },
                    { "22103110302", "123456", "GV" },
                    { "22103110303", "123456", "GV" },
                    { "22103199901", "admin", "CTSV" },
                    { "22103199902", "admin", "CTSV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}
