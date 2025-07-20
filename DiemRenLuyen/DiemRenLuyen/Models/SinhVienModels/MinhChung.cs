using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("MinhChung")]
    public class MinhChung
    {
        [Key]
        [StringLength(15)]
        public string MaMC { get; set; } = null!;

        [ForeignKey("PhieuDanhGia")]
        public string MaPhieu { get; set; } = null!;
        public PhieuDanhGia PhieuDanhGia { get; set; } = null!;

        [ForeignKey("TieuChi")]
        public string MaTC { get; set; } = null!;
        public TieuChi TieuChi { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string TepDinhKem { get; set; } = null!;

        [StringLength(100)]
        public string? MoTa { get; set; }
        //BO XUNG
        public ChiTietPhieu ChiTietPhieu { get; set; } = null!;
    }
}
