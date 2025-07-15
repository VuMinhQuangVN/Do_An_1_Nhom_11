using DiemRenLuyen.Data;
using DiemRenLuyen.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiemRenLuyen.Controllers
{
    public class TaiKhoansController : Controller
    {
        private readonly DiemRenLuyenContext _context;
        public TaiKhoansController(DiemRenLuyenContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaiKhoan model)
        {
            var user = _context.TaiKhoan.FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap && x.MatKhau == model.MatKhau);
            if (user != null)
            {
                // Đăng nhập thành công → chuyển trang theo vai trò
                if (user.VaiTro == "SV")
                    return RedirectToAction("Index", "SinhViens");  // Chưa có controller này thì sẽ báo lỗi
                else if (user.VaiTro == "GV")
                    return RedirectToAction("Index", "GiangViens");
                else if (user.VaiTro == "CTSV")
                    return RedirectToAction("Index", "CTSVs");
            }

            ViewBag.ThongBao = "Đăng nhập thất bại. Kiểm tra lại tài khoản hoặc mật khẩu!";
            return View("Index");
        }
    }
}
