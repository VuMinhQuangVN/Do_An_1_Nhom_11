using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("PhieuDanhGia")]
    public class PhieuDanhGia
    {
        [Key]
        [StringLength(15)]
        public string MaPhieu { get; set; } = null!;

        [ForeignKey("SinhVien")]
        public string MaSV { get; set; } = null!;
        public SinhVien SinhVien { get; set; } = null!;

        [ForeignKey("DotDanhGia")]
        public string MaDot { get; set; } = null!;
        public DotDanhGia DotDanhGia { get; set; } = null!;

        public DateTime NgayNop { get; set; }

        public double TongDiem { get; set; }

        public bool TrangThai { get; set; } = false;

        public bool DaXemKetQua { get; set; } = false;

        // ✅ Sinh viên đã gửi khiếu nại hay chưa (mặc định false)
        public bool PhanHoiTuSinhVien { get; set; } = false;

        // ✅ Giảng viên đã xử lý phản hồi hay chưa (mặc định false)
        public bool GiangVien_DaXetPhanHoi { get; set; } = false;

        // ✅ Nội dung phản hồi
        [StringLength(500)]
        public string? NoiDungPhanHoi { get; set; }

        // ✅ Thời gian phản hồi
        public DateTime? NgayPhanHoi { get; set; }

        public ICollection<ChiTietPhieu> ChiTietPhieus { get; set; } = new List<ChiTietPhieu>();
    }
}
