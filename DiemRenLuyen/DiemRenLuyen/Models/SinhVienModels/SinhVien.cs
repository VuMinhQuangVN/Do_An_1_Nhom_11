using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [StringLength(15)]
        public string MaSV { get; set; } = null!;
        [Required, StringLength(50)]
        public string HoTen { get; set; } = null!;

        [Required]
        public bool GioiTinh { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required, StringLength(100)]
        public string QueQuan { get; set; } = null!;

        [Required, ForeignKey("Lop")]
        public string MaLop { get; set; } = null!;

        public Lop Lop { get; set; } = null!;

        public ICollection<PhieuDanhGia> PhieuDanhGias { get; set; } = new List<PhieuDanhGia>();
    }
}
