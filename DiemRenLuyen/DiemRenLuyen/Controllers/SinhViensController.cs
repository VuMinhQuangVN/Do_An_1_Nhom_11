using DiemRenLuyen.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using DiemRenLuyen.Models.SinhVienViewModels;
using DiemRenLuyen.Models.SinhVienModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DiemRenLuyen.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly DiemRenLuyenContext _context;

        public SinhViensController(DiemRenLuyenContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var maSV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maSV))
                return RedirectToAction("Index", "TaiKhoans"); // Chưa đăng nhập

            var sinhVien = _context.SinhVien.FirstOrDefault(s => s.MaSV == maSV);
            return View(sinhVien);
        }

        public IActionResult TuChamDiem()
        {
            // Lấy mã sinh viên từ session (đăng nhập)
            var maSV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maSV))
                return RedirectToAction("Index", "Home");

            // Lấy thông tin sinh viên
            var sv = _context.SinhVien
                             .Include(s => s.Lop)
                             .FirstOrDefault(s => s.MaSV == maSV);

            if (sv == null) return NotFound();


            var today = DateTime.Today;

            var dotDanhGia = _context.DotDanhGias
                .FirstOrDefault(dot => dot.ThoiGianBatDau <= today && dot.ThoiGianKetThuc >= today);

            if (dotDanhGia == null)
            {
                ViewBag.ThongBao = "Hiện không trong thời gian đánh giá rèn luyện!";
                return View("KhongTrongThoiGian");
            }

            var phieuDaCo = _context.PhieuDanhGia
                .FirstOrDefault(p => p.MaSV == maSV && p.MaDot == dotDanhGia.MaDot);

            if (phieuDaCo != null)
            {
                var model = new TuChamDiemViewModel
                {
                    MaSV = phieuDaCo.MaSV,
                    HoTen = sv.HoTen,
                    Lop = sv.MaLop,
                    DotDanhGia = $"{dotDanhGia.HocKy} - {dotDanhGia.NamHoc}",
                    NgayNop = phieuDaCo.NgayNop
                };

                ViewBag.DaDanhGia = true;
                return View("DaDanhGia", model); // View này chỉ hiển thị thông tin + thông báo
            }


            // Lấy danh sách tiêu chí
            var dsTieuChi = _context.TieuChi
                                    .Select(tc => new TieuChiChamDiemVM
                                    {
                                        MaTC = tc.MaTC,
                                        TenTieuChi = tc.TenTieuChi,
                                        MucDiemToiDa = tc.MucDiemToiDa
                                    }).ToList();

            var vm = new TuChamDiemViewModel
            {
                MaSV = sv.MaSV,
                HoTen = sv.HoTen,
                Lop = sv.MaLop,
                DotDanhGia = $"{dotDanhGia.HocKy} - {dotDanhGia.NamHoc}",
                DanhSachTieuChi = dsTieuChi
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> TuChamDiem(TuChamDiemViewModel model)
        {
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                if (state.Errors.Count > 0)
                {
                    for (int i = 0; i < state.Errors.Count; i++)
                    {
                        var errorMessage = state.Errors[i].ErrorMessage;
                        if (errorMessage.Contains("not valid for"))  // phát hiện lỗi kiểu binding
                        {
                            state.Errors[i] = new ModelError("Điểm phải là một số.");
                        }
                    }
                }
            }

            for (int i = 0; i < model.DanhSachTieuChi.Count; i++)
            {
                var tc = model.DanhSachTieuChi[i];
                if (tc.DiemTuCham < 0)
                {
                    ModelState.AddModelError($"DanhSachTieuChi[{i}].DiemTuCham", "Không được nhập điểm âm.");
                }
                else if (tc.DiemTuCham > tc.MucDiemToiDa)
                {
                    ModelState.AddModelError($"DanhSachTieuChi[{i}].DiemTuCham", $"Không được vượt quá {tc.MucDiemToiDa} điểm.");
                }
            }

            if (!ModelState.IsValid)
            {
                // Lấy lại dữ liệu sinh viên để điền lại form
                var sv = _context.SinhVien
                                 .Include(s => s.Lop)
                                 .FirstOrDefault(s => s.MaSV == model.MaSV);

                var dot = _context.DotDanhGias.FirstOrDefault();

                model.MaSV = sv?.MaSV ?? "";
                model.HoTen = sv?.HoTen ?? "";
                model.Lop = sv?.MaLop ?? "";
                model.DotDanhGia = dot != null ? $"{dot.HocKy} - {dot.NamHoc}" : "";
                model.NgayNop = DateTime.Now;

                // Lấy lại danh sách tiêu chí
                model.DanhSachTieuChi = _context.TieuChi.Select(tc => new TieuChiChamDiemVM
                {
                    MaTC = tc.MaTC,
                    TenTieuChi = tc.TenTieuChi,
                    MucDiemToiDa = tc.MucDiemToiDa
                }).ToList();

                return View(model);
            }

            // Tính điểm nhóm B (lấy điểm cao nhất trong từng nhóm 3 ký tự đầu)
            var nhomB_Grouped = model.NhomB
                .GroupBy(tc => tc.MaTC.Length >= 3 ? tc.MaTC.Substring(0, 3) : tc.MaTC)
                .Select(g =>
                {
                    if (g.Count() == 1)
                        return g.First().DiemTuCham;
                    else
                        return g.Max(tc => tc.DiemTuCham);
                })
                .Sum();
            // Điểm nhóm C (tổng hết)
            var nhomC_Tong = model.NhomC.Sum(tc => tc.DiemTuCham);

            float tongDiem = 65 + (float)nhomB_Grouped - (float)nhomC_Tong;


            if(tongDiem > 100)
            {
                tongDiem = 100;
            }

            // Tạo mã phiếu (có thể tự động tăng hoặc theo logic riêng)
            string maPhieu = "PDG" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            var phieu = new PhieuDanhGia
            {
                MaPhieu = maPhieu,
                MaSV = model.MaSV,
                MaDot = _context.DotDanhGias.FirstOrDefault()?.MaDot ?? "DG001",
                NgayNop = DateTime.Now,
                TongDiem = tongDiem,
                TrangThai = false,
                DaXemKetQua = false
            };

            try
            {
                _context.PhieuDanhGia.Add(phieu);
                await _context.SaveChangesAsync(); // 💥 nếu lỗi sẽ dừng tại đây

                Console.WriteLine("Thêm phiếu thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("LỖI khi thêm phiếu: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                return Content("Lỗi khi thêm phiếu: " + ex.Message);
            }
            // Lưu chi tiết và minh chứng
            foreach (var tc in model.DanhSachTieuChi)
            {
                var chitiet = new ChiTietPhieu
                {
                    MaPhieu = maPhieu,
                    MaTC = tc.MaTC,
                    DiemSV = (float)tc.DiemTuCham
                };
                _context.ChiTietPhieu.Add(chitiet);

                // Nếu có minh chứng
                if (tc.FileMinhChung != null && tc.FileMinhChung.Length > 0)
                {
                    // Tạo tên file duy nhất
                    string uniqueFileName = $"{Guid.NewGuid().ToString("N")}_{tc.FileMinhChung.FileName}";
                    string savePath = Path.Combine("wwwroot/minhchung", uniqueFileName);

                    // Đảm bảo thư mục tồn tại
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await tc.FileMinhChung.CopyToAsync(stream);
                    }

                    var minhChung = new MinhChung
                    {
                        MaMC = "MC" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                        MaPhieu = maPhieu,
                        MaTC = tc.MaTC,
                        TepDinhKem = uniqueFileName,
                        MoTa = $"Minh chứng cho tiêu chí {tc.MaTC}"
                    };

                    _context.MinhChung.Add(minhChung);
                }               
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("KetQua", new { id = maPhieu });
        }

        public IActionResult KetQua(string? maDot)
        {
            var maSV = HttpContext.Session.GetString("Ma");
            if (string.IsNullOrEmpty(maSV)) return RedirectToAction("Index", "Home");

            var sv = _context.SinhVien.FirstOrDefault(x => x.MaSV == maSV);
            if (sv == null) return NotFound();

            var dots = _context.DotDanhGias.ToList();
            maDot ??= dots.FirstOrDefault()?.MaDot;

            var phieu = _context.PhieuDanhGia
                .FirstOrDefault(p => p.MaSV == maSV && p.MaDot == maDot);

            List<ChiTietPhieuViewModel> chiTiet = new();

            if (phieu != null)
            {
                chiTiet = (from ct in _context.ChiTietPhieu
                           join tc in _context.TieuChi on ct.MaTC equals tc.MaTC
                           join mc in _context.MinhChung on new { ct.MaPhieu, ct.MaTC } equals new { mc.MaPhieu, mc.MaTC } into g
                           from mc in g.DefaultIfEmpty()
                           where ct.MaPhieu == phieu.MaPhieu
                           select new ChiTietPhieuViewModel
                           {
                               MaTC = ct.MaTC,
                               TenTieuChi = tc.TenTieuChi,
                               DiemSV = (float)ct.DiemSV,
                               TepMinhChung = mc != null ? "/minhchung/" + mc.TepDinhKem : null
                           }).ToList();
            }

            // Lấy trạng thái duyệt khoa từ bảng DuyetPhieu (nếu có)
            var duyetPhieu = _context.DuyetPhieu
                .FirstOrDefault(dp => dp.MaPhieu == phieu.MaPhieu);

            var vm = new KetQuaViewModel
            {
                MaSV = sv.MaSV,
                HoTen = sv.HoTen,
                MaDotDuocChon = maDot!,
                DanhSachDot = dots,
                PhanHoi = phieu,
                ChiTiet = chiTiet,

                TrangThaiDuyetGV = phieu?.TrangThai ?? false,
                TrangThaiGuiKhoa = duyetPhieu?.TrangThai ?? false,
                PhanHoiTuSinhVien = phieu?.PhanHoiTuSinhVien ?? false,
                GiangVien_DaXetPhanHoi = phieu?.GiangVien_DaXetPhanHoi ?? false
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult GuiPhanHoi(string maPhieu)
        {
            var maSV = HttpContext.Session.GetString("Ma");

            if (string.IsNullOrEmpty(maSV))
                return RedirectToAction("Index", "Home");

            // Lấy thông tin sinh viên
            var sv = _context.SinhVien
                             .Include(s => s.Lop)
                             .FirstOrDefault(s => s.MaSV == maSV);

            if (sv == null) return NotFound();
            var phieu = _context.PhieuDanhGia.FirstOrDefault(p => p.MaPhieu == maPhieu);
            if (phieu == null) return NotFound();

            if (phieu.PhanHoiTuSinhVien) return RedirectToAction("KetQua"); // chỉ cho phản hồi 1 lần

            var vm = new PhanHoiViewModel { MaSV = maSV, HoTen = sv.HoTen ,MaPhieu = maPhieu };
            return View(vm);
        }

        [HttpPost]
        public IActionResult GuiPhanHoi(PhanHoiViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var phieu = _context.PhieuDanhGia.FirstOrDefault(p => p.MaPhieu == model.MaPhieu);
            if (phieu == null) return NotFound();

            // Cập nhật nội dung phản hồi
            phieu.NoiDungPhanHoi = model.NoiDungPhanHoi;
            phieu.NgayPhanHoi = DateTime.Now;
            phieu.PhanHoiTuSinhVien = true;

            // ✅ Nếu có tệp đính kèm
            if (model.TepPhanHoi != null)
            {
                var tenFile = Path.GetFileName(model.TepPhanHoi.FileName);
                var path = Path.Combine("wwwroot/phanhoi", tenFile);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.TepPhanHoi.CopyTo(stream);
                }

                // Lưu tên file nếu muốn hiển thị lại (có thể tạo cột riêng trong DB nếu cần)
                phieu.NoiDungPhanHoi += $"\n[Đính kèm: /phanhoi/{tenFile}]";
            }

            _context.SaveChanges();

            TempData["ThongBao"] = "Gửi phản hồi thành công.";
            return RedirectToAction("KetQua");
        }
    }
}
