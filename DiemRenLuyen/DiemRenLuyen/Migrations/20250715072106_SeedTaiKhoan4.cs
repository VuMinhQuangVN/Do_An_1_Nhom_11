using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiemRenLuyen.Migrations
{
    /// <inheritdoc />
    public partial class SeedTaiKhoan4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.DeleteData(
            table: "TaiKhoan",
            keyColumn: "TenDangNhap",
            keyValues: new object[]
            {
                "22103100228",
                "22103100229",
                "22103100230",
                "22103100231",
                "22103100232",
                "22103110301",
                "22103110302",
                "22103110303",
                "22103199901",
                "22103199902"
            });
        }
    }
}
