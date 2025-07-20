using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("ChiTietPhieu")]
    public class ChiTietPhieu
    {
        [ForeignKey("PhieuDanhGia")]
        [StringLength(15)]
        public string MaPhieu { get; set; } = null!;
        public PhieuDanhGia PhieuDanhGia { get; set; } = null!;

        [ForeignKey("TieuChi")]
        [StringLength(15)]
        public string MaTC { get; set; } = null!;
        public TieuChi TieuChi { get; set; } = null!;

        public double DiemSV { get; set; }

        [StringLength(200)]
        public string? GhiChu { get; set; }

        // Mối quan hệ 1 - N: Mỗi Chi tiết phiếu có thể có nhiều minh chứng
        public ICollection<MinhChung> MinhChungs { get; set; } = new List<MinhChung>();
    }
}
