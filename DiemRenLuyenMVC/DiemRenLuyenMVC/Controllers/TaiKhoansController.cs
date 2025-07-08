using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiemRenLuyenMVC.Data;
using DiemRenLuyenMVC.Models;

namespace DiemRenLuyenMVC.Controllers
{
    public class TaiKhoansController : Controller
    {
        private readonly DiemRenLuyenMVCContext _context;

        public TaiKhoansController(DiemRenLuyenMVCContext context)
        {
            _context = context;
        }

        // GET: TaiKhoans
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaiKhoan model)
        {
            var user =  _context.TaiKhoan.FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap && x.MatKhau == model.MatKhau);
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
