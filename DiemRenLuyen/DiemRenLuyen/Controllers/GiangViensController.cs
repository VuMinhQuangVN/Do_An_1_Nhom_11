using DiemRenLuyen.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using DiemRenLuyen.Models.GiangVienViewModels;
using DiemRenLuyen.Models.GiangVienModels;
using DiemRenLuyen.Models.SinhVienModels;
namespace DiemRenLuyen.Controllers
{
    public class GiangViensController : Controller
    {
        private readonly DiemRenLuyenContext _context;

        public GiangViensController(DiemRenLuyenContext context)
        {
            _context = context;
        }

        // Trang chính
        public IActionResult Index()
        {
            var maGV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maGV))
                return RedirectToAction("Index", "TaiKhoans"); // Chưa đăng nhập

            var gv = _context.GiangVien.FirstOrDefault(s => s.MaGV == maGV);
            return View(gv);
        }

        // Danh sách phiếu chưa duyệt từ sinh viên thuộc lớp mà giảng viên phụ trách
        public IActionResult DanhSachPhieu(string? maLop, string? maKhoa, string? maDot)
        {
            var maGV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maGV)) return RedirectToAction("Index", "TaiKhoans");

            // Lấy danh sách lớp giảng viên phụ trách
            var dsLop = _context.Lop.Where(l => l.MaGV == maGV).ToList();
            ViewBag.DanhSachLop = dsLop;
            ViewBag.LopDangChon = maLop;

            // Lấy danh sách khoa
            var dsKhoa = _context.Khoa.ToList();
            ViewBag.DanhSachKhoa = dsKhoa;
            ViewBag.KhoaDangChon = maKhoa;

            // Lấy danh sách đợt đánh giá
            var dsDot = _context.DotDanhGias.ToList();
            ViewBag.DanhSachDot = dsDot;
            ViewBag.DotDangChon = maDot;

            // Lọc theo các điều kiện
            var query = _context.PhieuDanhGia
                .Include(p => p.SinhVien)
                .ThenInclude(sv => sv.Lop)
                .AsQueryable();
            var gv = _context.GiangVien.FirstOrDefault(g => g.MaGV == maGV);
            if (gv == null) return NotFound();

            ViewBag.TenGV = gv.TenGV;
            if (!string.IsNullOrEmpty(maLop))
                query = query.Where(p => p.SinhVien.MaLop == maLop);

            if (!string.IsNullOrEmpty(maKhoa))
                query = query.Where(p => p.SinhVien.Lop.MaKhoa == maKhoa);

            if (!string.IsNullOrEmpty(maDot))
                query = query.Where(p => p.MaDot == maDot);

            query = query.Where(p => p.SinhVien.Lop.MaGV == maGV && !p.TrangThai)
                         .OrderBy(p => p.NgayNop);

            return View(query.ToList());
        }


        // Xem chi tiết phiếu để duyệt
        public IActionResult DuyetPhieu(string maPhieu)
        {
            if (string.IsNullOrEmpty(maPhieu)) return NotFound();

            var phieu = _context.PhieuDanhGia
                .Include(p => p.SinhVien)
                .FirstOrDefault(p => p.MaPhieu == maPhieu);
            if (phieu == null) return NotFound();

            var chiTiet = _context.ChiTietPhieu
                .Include(ct => ct.TieuChi)
                .Where(ct => ct.MaPhieu == maPhieu)
                .ToList();

            var minhChung = _context.MinhChung
                .Where(mc => mc.MaPhieu == maPhieu)
                .ToList();

            var vm = new DuyetPhieuViewModel
            {
                MaPhieu = phieu.MaPhieu,
                MaSV = phieu.MaSV,
                HoTenSV = phieu.SinhVien.HoTen,
                NgayNop = phieu.NgayNop,
                DanhSachChiTiet = chiTiet.Select(ct => new ChiTietPhieuViewModel
                {
                    MaTC = ct.MaTC,
                    TenTieuChi = ct.TieuChi.TenTieuChi,
                    DiemSV = (float)ct.DiemSV,
                    TepMinhChung = minhChung.FirstOrDefault(m => m.MaTC == ct.MaTC)?.TepDinhKem
                }).ToList()
            };

            return View(vm);
        }

        // POST: Lưu điểm chỉnh sửa và duyệt phiếu
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> DuyetPhieu(DuyetPhieuPostModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dữ liệu không hợp lệ.");

            var phieu = await _context.PhieuDanhGia.FindAsync(model.MaPhieu);
            if (phieu == null) return NotFound();

            // Cập nhật từng chi tiết điểm
            for (int i = 0; i < model.MaTCs.Count; i++)
            {
                var chiTiet = await _context.ChiTietPhieu
                    .FirstOrDefaultAsync(c => c.MaPhieu == model.MaPhieu && c.MaTC == model.MaTCs[i]);

                if (chiTiet != null)
                    chiTiet.DiemSV = model.Diems[i];
            }

            // ⚙️ Tính điểm giống sinh viên:
            var nhomB_Grouped = model.MaTCs
                .Select((maTC, i) => new { MaTC = maTC, Diem = model.Diems[i] })
                .Where(x => x.MaTC.StartsWith("B"))
                .GroupBy(x => x.MaTC.Length >= 3 ? x.MaTC.Substring(0, 3) : x.MaTC)
                .Select(g => g.Max(x => x.Diem))
                .Sum();

            var nhomC_Tong = model.MaTCs
                .Select((maTC, i) => new { MaTC = maTC, Diem = model.Diems[i] })
                .Where(x => x.MaTC.StartsWith("C"))
                .Sum(x => x.Diem);

            float tong = 65 + nhomB_Grouped - nhomC_Tong;

            // Cập nhật phiếu
            phieu.TrangThai = true; // Đã duyệt bởi giảng viên

            var maGV = HttpContext.Session.GetString("Ma");

            // Ghi vào bảng DuyetPhieu
            _context.DuyetPhieu.Add(new DuyetPhieu
            {
                MaPhieu = model.MaPhieu,
                MaGV = maGV!,
                NgayDuyet = DateTime.Now,
                DiemDeXuat = tong,
                GhiChu = model.GhiChu,
                TrangThai = false 
                //Sửa cái này
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("DanhSachPhieu");
        }


        // (Tuỳ chọn) Danh sách đã duyệt
        public IActionResult DaDuyet(string? maLop)
        {
            var maGV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maGV))
                return RedirectToAction("Index", "TaiKhoans");

            var dsLop = _context.Lop.Where(l => l.MaGV == maGV).ToList();
            ViewBag.DanhSachLop = dsLop;
            ViewBag.LopDangChon = maLop;

            var query = _context.DuyetPhieu
                .Include(d => d.PhieuDanhGia)
                .ThenInclude(p => p.SinhVien)
                .ThenInclude(sv => sv.Lop)
                .Where(d => d.MaGV == maGV);

            if (!string.IsNullOrEmpty(maLop))
                query = query.Where(d => d.PhieuDanhGia.SinhVien.MaLop == maLop);

            var list = query.OrderByDescending(d => d.NgayDuyet).ToList();
            return View(list);
        }
        public async Task<IActionResult> GuiLenKhoa(string maLop)
        {
            var maGV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maGV)) return RedirectToAction("Index", "TaiKhoans");

            // Lấy tất cả phiếu đã duyệt nhưng chưa gửi của lớp đó
            //var danhSach = _context.DuyetPhieu
            //    .Where(dp => dp.PhieuDanhGia.SinhVien.MaLop == maLop
            //              && dp.MaGV == maGV
            //              && dp.TrangThai == false) // chưa gửi
            //    .ToList();
            var danhSach = _context.DuyetPhieu
                .Include(dp => dp.PhieuDanhGia)
                .ThenInclude(pd => pd.SinhVien)
                .Where(dp => dp.PhieuDanhGia.SinhVien.MaLop == maLop
                    && dp.MaGV == maGV
                    && dp.TrangThai == false)
                    .ToList();

            // ✅ DEBUG: Kiểm tra có lấy đúng dữ liệu không
            Console.WriteLine($"maGV session: {maGV}");
            Console.WriteLine($"maLop: {maLop}");
            Console.WriteLine($"Số lượng danh sách: {danhSach.Count}");

            // ✅ Xem từng mã phiếu lấy được
            foreach (var item in danhSach)
            {
                Console.WriteLine($"→ MaPhieu: {item.MaPhieu}, TrangThai hiện tại: {item.TrangThai}");
                item.TrangThai = true;
            }
            await _context.SaveChangesAsync();
            TempData["Message"] = "Đã gửi toàn bộ phiếu đã duyệt lên khoa.";
            return RedirectToAction("DaDuyet", new { maLop });
        }


    }
}

