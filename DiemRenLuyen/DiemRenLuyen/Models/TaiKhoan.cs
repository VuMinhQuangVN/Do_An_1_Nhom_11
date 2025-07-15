using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiemRenLuyen.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Tên đăng nhập phải đúng 11 ký tự.")]
        public string? TenDangNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string? MatKhau { get; set; }
        [Required]
        public string? VaiTro { get; set; }
    }
}
