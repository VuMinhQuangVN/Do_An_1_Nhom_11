using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiemRenLuyen.Migrations
{
    /// <inheritdoc />
    public partial class SeedTieuChiData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TieuChi",
                columns: new[] { "MaTC", "TenTieuChi", "MucDiemToiDa", "MoTa" },
                values: new object[,]
                {
            { "B11", "Hoạt động Đoàn – Hội", 0.0, null },
            { "B111", "Tham gia CLB học thuật", 5.0, "Có minh chứng (ảnh/giấy xác nhận)" },
            { "B112", "Tham gia các cuộc thi học thuật", 10.0, "Giấy khen, ảnh, xác nhận" },
            { "B21", "Công tác xã hội", 7.0, "Có xác nhận tham gia" },
            { "C11", "Đi học muộn quá 3 buổi", 5.0, "Trừ nếu vi phạm" },
            { "C21", "Không tham gia sinh hoạt lớp", 3.0, "Trừ nếu không có lý do chính đáng" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "TieuChi", keyColumn: "MaTC", keyValue: "B11");
            migrationBuilder.DeleteData(table: "TieuChi", keyColumn: "MaTC", keyValue: "B111");
            migrationBuilder.DeleteData(table: "TieuChi", keyColumn: "MaTC", keyValue: "B112");
            migrationBuilder.DeleteData(table: "TieuChi", keyColumn: "MaTC", keyValue: "B21");
            migrationBuilder.DeleteData(table: "TieuChi", keyColumn: "MaTC", keyValue: "C11");
            migrationBuilder.DeleteData(table: "TieuChi", keyColumn: "MaTC", keyValue: "C21");
        }

    }
}
