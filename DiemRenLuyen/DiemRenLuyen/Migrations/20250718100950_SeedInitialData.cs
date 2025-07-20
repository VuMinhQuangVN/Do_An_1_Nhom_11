using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiemRenLuyen.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DotDanhGia",
                columns: new[] { "MaDot", "HocKy", "NamHoc", "ThoiGianBatDau", "ThoiGianKetThuc" },
                values: new object[] { "DG001", 1, "2024–2025", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
               table: "Khoa",
               columns: new[] { "MaKhoa", "MaGVTruongKhoa", "TenKhoa" },
               values: new object[] { "CNTT", "10103100001", "Công nghệ thông tin" });

            migrationBuilder.InsertData(
                table: "GiangVien",
                columns: new[] { "MaGV", "DienThoai", "Email", "MaKhoa", "TenGV" },
                values: new object[,]
                {
                    { "13103100200", "0901234567", "nguyenvana@example.com", "CNTT", "Nguyễn Văn A" },
                    { "13103100201", "0909876543", "tranthingoc@example.com", "CNTT", "Trần Thị Ngọc" }  // <-- Bổ sung GV này
                });


            migrationBuilder.InsertData(
                table: "TieuChi",
                columns: new[] { "MaTC", "MoTa", "MucDiemToiDa", "TenTieuChi" },
                values: new object[,]
                {
                    { "TC01", " ", 10f, "Tham gia học tập tốt" },
                    { "TC02", " ", 15f, "Tích cực tham gia hoạt động lớp" }
                });

            migrationBuilder.InsertData(
                table: "Lop",
                columns: new[] { "MaLop", "MaGV", "MaKhoa", "TenLop" },
                values: new object[,]
                {
                    { "DHTI16A1HN", "13103100200", "CNTT", "Công nghệ thông tin 1" },
                    { "DHTI16A2HN", "13103100200", "CNTT", "Công nghệ thông tin 2" },
                    { "DHTI16A3HN", "13103100201", "CNTT", "Công nghệ thông tin 3" }
                });

            migrationBuilder.InsertData(
                table: "SinhVien",
                columns: new[] { "MaSV", "GioiTinh", "HoTen", "MaLop", "NgaySinh", "QueQuan" },
                values: new object[] { "22103100228", true, "Trần Văn B", "DHTI16A1HN", new DateTime(2003, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ninh Bình" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SinhVien",
                keyColumn: "MaSV",
                keyValue: "22103100228");

            migrationBuilder.DeleteData(
                table: "Lop",
                keyColumn: "MaLop",
                keyValue: "DHTI16A1HN");

            migrationBuilder.DeleteData(
                table: "Lop",
                keyColumn: "MaLop",
                keyValue: "DHTI16A2HN");

            migrationBuilder.DeleteData(
                table: "Lop",
                keyColumn: "MaLop",
                keyValue: "DHTI16A3HN");

            migrationBuilder.DeleteData(
                table: "GiangVien",
                keyColumn: "MaGV",
                keyValue: "13103100200");

            migrationBuilder.DeleteData(
                table: "Khoa",
                keyColumn: "MaKhoa",
                keyValue: "CNTT");

            migrationBuilder.DeleteData(
                table: "TieuChi",
                keyColumn: "MaTC",
                keyValue: "TC01");

            migrationBuilder.DeleteData(
                table: "TieuChi",
                keyColumn: "MaTC",
                keyValue: "TC02");

            migrationBuilder.DeleteData(
                table: "DotDanhGia",
                keyColumn: "MaDot",
                keyValue: "DG001");
        }
    }
}
