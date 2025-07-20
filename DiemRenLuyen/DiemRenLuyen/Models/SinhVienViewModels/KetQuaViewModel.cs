using DiemRenLuyen.Models.SinhVienModels;

namespace DiemRenLuyen.Models.SinhVienViewModels
{
    public class KetQuaViewModel
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }

        public string MaDotDuocChon { get; set; }
        public List<DotDanhGia> DanhSachDot { get; set; }

        public PhieuDanhGia? PhanHoi { get; set; }

        public List<ChiTietPhieuViewModel> ChiTiet { get; set; } = new();

        // Trạng thái xử lý
        public bool TrangThaiDuyetGV { get; set; }         // = PhieuDanhGia.TrangThai
        public bool TrangThaiGuiKhoa { get; set; }         // = DuyetPhieu.TrangThai

        public bool PhanHoiTuSinhVien { get; set; }        // Sinh viên đã gửi khiếu nại?
        public bool GiangVien_DaXetPhanHoi { get; set; }   // Giảng viên đã xử lý phản hồi?
    }

    public class ChiTietPhieuViewModel
    {
        public string MaTC { get; set; }
        public string TenTieuChi { get; set; }
        public float DiemSV { get; set; }
        public string? TepMinhChung { get; set; }
    }
}
