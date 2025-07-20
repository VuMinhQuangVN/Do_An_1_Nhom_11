using System.ComponentModel.DataAnnotations;

namespace DiemRenLuyen.Models.SinhVienViewModels
{

    public class TieuChiChamDiemVM
    {
        public string MaTC { get; set; }
        public string TenTieuChi { get; set; }
        public double MucDiemToiDa { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập điểm.")]
        [Range(0, 100, ErrorMessage = "Điểm phải nằm trong khoảng hợp lệ")]
        public double DiemTuCham { get; set; }

        public IFormFile? FileMinhChung { get; set; }
    }

    public class TuChamDiemViewModel
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string Lop { get; set; }
        public string DotDanhGia { get; set; } // ví dụ: "HK1 - 2024–2025"
        public DateTime NgayNop { get; set; } = DateTime.Now;

        public List<TieuChiChamDiemVM> DanhSachTieuChi { get; set; }

        public List<TieuChiChamDiemVM> NhomB =>
            DanhSachTieuChi != null
            ? DanhSachTieuChi.Where(tc => tc.MaTC != null && tc.MaTC.StartsWith("B")).ToList()
            : new List<TieuChiChamDiemVM>();

        public List<TieuChiChamDiemVM> NhomC =>
            DanhSachTieuChi != null
            ? DanhSachTieuChi.Where(tc => tc.MaTC != null && tc.MaTC.StartsWith("C")).ToList()
            : new List<TieuChiChamDiemVM>();
    }
}
