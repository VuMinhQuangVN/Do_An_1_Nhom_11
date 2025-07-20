using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("TieuChi")]
    public class TieuChi
    {
        [Key]
        [StringLength(15)]
        public string MaTC { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string TenTieuChi { get; set; } = null!;

        [Required]
        public double MucDiemToiDa { get; set; }

        [StringLength(500)]
        public string? MoTa { get; set; }

        // Quan hệ với ChiTietPhieu, MinhChung
        public ICollection<ChiTietPhieu> ChiTietPhieus { get; set; } = new List<ChiTietPhieu>();
    }
}
