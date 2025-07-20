using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("Lop")]
    public class Lop
    {
        [Key]
        [StringLength(15)]
        public string MaLop { get; set; } = null!;

        [Required, StringLength(50)]
        public string TenLop { get; set; } = null!;

        [Required, ForeignKey("GiangVien")]
        public string MaGV { get; set; } = null!;

        public GiangVien GiangVien { get; set; } = null!;

        [Required, ForeignKey("Khoa")]
        public string MaKhoa { get; set; } = null!;
        public Khoa Khoa { get; set; } = null!;
        public ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
    }
}
