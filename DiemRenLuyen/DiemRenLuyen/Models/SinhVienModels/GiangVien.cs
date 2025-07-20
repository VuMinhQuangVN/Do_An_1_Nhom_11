using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("GiangVien")]
    public class GiangVien
    {
        [Key]
        [StringLength(15)]
        public string MaGV { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string TenGV { get; set; } = null!;

        [Required, StringLength(50)]
        public string Email { get; set; } = null!;

        [Required, StringLength(15)]
        public string DienThoai { get; set; } = null!;
        [Required]
        [ForeignKey("Khoa")]
        public string MaKhoa { get; set; } = null!;
        public Khoa Khoa { get; set; } = null!;

        // Một giảng viên có thể chủ nhiệm nhiều lớp
        public ICollection<Lop> Lops { get; set; } = new List<Lop>();
    }
}
