using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienViewModels
{
    public class PhanHoiViewModel
    {
        public string MaSV { get; set; }

        public string HoTen { get; set; }

        public string MaPhieu { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung phản hồi.")]
        public string NoiDungPhanHoi { get; set; }

        public IFormFile? TepPhanHoi { get; set; }
    }
}
