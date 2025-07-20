using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiemRenLuyen.Models.SinhVienModels
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        [StringLength(15)]
        public string MaKhoa { get; set; } = null!;

        [Required, StringLength(50)]
        public string TenKhoa { get; set; } = null!;

        [StringLength(15)]
        public string? MaGVTruongKhoa { get; set; }

        public ICollection<Lop> Lops { get; set; } = new List<Lop>();
    }
}
