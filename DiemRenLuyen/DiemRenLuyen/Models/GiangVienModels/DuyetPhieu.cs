using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DiemRenLuyen.Models.SinhVienModels;

namespace DiemRenLuyen.Models.GiangVienModels
{
    [Table("DuyetPhieu")]
    public class DuyetPhieu
    {
        [Key]
        [Required]
        public string MaPhieu { get; set; } = null!;

        [Required]
        [Column("MaGV_Duyet")]
        public string MaGV { get; set; } = null!;

        [Required]
        public DateTime NgayDuyet { get; set; }

        [Required]
        public double DiemDeXuat { get; set; }

        public string? GhiChu { get; set; }

        public bool TrangThai { get; set; } = false;

        // === Navigation Properties ===
        [ForeignKey("MaPhieu")]
        public PhieuDanhGia? PhieuDanhGia { get; set; }

        [ForeignKey("MaGV")]
        public GiangVien? GiangVien { get; set; }
    }
}
