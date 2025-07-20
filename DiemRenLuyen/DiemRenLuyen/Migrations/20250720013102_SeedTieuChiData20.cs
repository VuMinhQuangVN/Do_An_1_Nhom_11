using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiemRenLuyen.Migrations
{
    /// <inheritdoc />
    public partial class SeedTieuChiData20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TieuChi",
                columns: new[] { "MaTC", "TenTieuChi", "MucDiemToiDa", "MoTa" },
                values: new object[,]
                {
            { "A", "Điểm rèn luyện mặc định của sinh viên trong học kỳ", 70.0, "Mặc định 70 điểm" },
            { "B", "Điểm rèn luyện được cộng trong kỳ (điểm cộng/lần, hoạt động...)", 0.0, " " },
            { "B1", "Điểm cộng về ý thức tham gia học tập", 0.0, " " },
            { "B11", "Đạt kết quả học tập từ 7.0 trở lên (theo thang điểm 10)", 0.0, " " },
            { "B111", "Đạt điểm trung bình học tập trên 9,0", 5.0, " " },
            { "B112", "Đạt điểm trung bình học tập từ 8,0 đến 9,0", 3.0, " " },
            { "B113", "Đạt điểm trung bình học tập từ 7,0 đến 8,0", 2.0, " " },
            { "B12", "Có tinh thần cố gắng vượt khó, vươn lên trong học tập", 2.0, " " },
            { "B13", "Tham gia các CLB học thuật, hoạt động NCKH, các cuộc thi khởi nghiệp sáng tạo", 100.0, " " },
            { "B2", "Điểm cộng về ý thức chấp hành nội quy, quy chế, quy định trong nhà trường", 0.0, " " },
            { "B21", "Có hành động tích cực trên mạng xã hội phù hợp với chủ trương của nhà trường", 100.0, " " },
            { "C", "Điểm rèn luyện bị trừ trong kỳ (điểm trừ/lần vi phạm, buổi...)", 0.0, " " },
            { "C1", "Điểm trừ về ý thức học tập", 0.0, " " },
            { "C11", "Vi phạm quy chế thi, kiểm tra", 4.0, null },
            { "C12", "Thi lại môn học, học phần", 2.0, null },
            { "C13", "Vi phạm thời gian học tập, thiếu ý thức trong học tập", 0.0, " " },
            { "C131", "Nghỉ học không có lý do", 100.0, " " },
            { "C132", "Đi học muộn, trốn tiết, bỏ giờ,..", 100.0, " " },
            { "C2", "Điểm trừ về ý thức chấp hành nội quy, quy chế nhà trường", 0.0, " " },
            { "C21", "Đi học hộ hoặc nhờ người khác đi học hộ", 100.0, " " },
            { "C22", "Không đóng học phí đúng thời hạn", 5.0, " " }
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string[] maTieuChi = new[]
            {
        "A", "B", "B1", "B11", "B111", "B112", "B113", "B12", "B13", "B2", "B21",
        "C", "C1", "C11", "C12", "C13", "C131", "C132", "C2", "C21", "C22"
    };

            foreach (var ma in maTieuChi)
            {
                migrationBuilder.DeleteData(
                    table: "TieuChi",
                    keyColumn: "MaTC",
                    keyValue: ma
                );
            }
        }
    }
}
