using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("DotDanhGia")]
    public class DotDanhGia
    {
        [Key]
        [StringLength(15)]
        public string MaDot { get; set; } = null!;

        [Required]
        [Range(1, 3, ErrorMessage = "Học kỳ phải là 1, 2 hoặc 3.")]
        public int HocKy { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Năm học phải có dạng ví dụ: 2024-2025")]
        public string NamHoc { get; set; } = null!;

        [Required]
        public DateTime ThoiGianBatDau { get; set; }

        [Required]
        public DateTime ThoiGianKetThuc { get; set; }

        // Quan hệ 1 - n với Phiếu đánh giá
        public ICollection<PhieuDanhGia> PhieuDanhGias { get; set; } = new List<PhieuDanhGia>();
    }
}
