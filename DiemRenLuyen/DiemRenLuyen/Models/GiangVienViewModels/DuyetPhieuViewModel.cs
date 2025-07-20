using DiemRenLuyen.Models.SinhVienViewModels;

namespace DiemRenLuyen.Models.GiangVienViewModels
{
    public class DuyetPhieuViewModel
    {
        public string MaPhieu { get; set; }
        public string MaSV { get; set; }
        public string HoTenSV { get; set; }
        public DateTime NgayNop { get; set; }
        public List<ChiTietPhieuViewModel> DanhSachChiTiet { get; set; }

        public List<ChiTietPhieuViewModel> NhomB =>
            DanhSachChiTiet != null
            ? DanhSachChiTiet.Where(tc => tc.MaTC != null && tc.MaTC.StartsWith("B")).ToList()
            : new List<ChiTietPhieuViewModel>();

        public List<ChiTietPhieuViewModel> NhomC =>
            DanhSachChiTiet != null
            ? DanhSachChiTiet.Where(tc => tc.MaTC != null && tc.MaTC.StartsWith("C")).ToList()
            : new List<ChiTietPhieuViewModel>();
    }
}
