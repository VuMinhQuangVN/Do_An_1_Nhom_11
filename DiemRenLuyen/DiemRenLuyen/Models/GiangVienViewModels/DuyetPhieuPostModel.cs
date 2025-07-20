namespace DiemRenLuyen.Models.GiangVienViewModels
{
    public class DuyetPhieuPostModel
    {
        public string MaPhieu { get; set; }
        public List<string> MaTCs { get; set; }
        public List<float> Diems { get; set; }
        public string GhiChu { get; set; } = "";
    }
}
