using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiemRenLuyenMVC.Models;

namespace DiemRenLuyenMVC.Data
{
    public class DiemRenLuyenMVCContext : DbContext
    {
        public DiemRenLuyenMVCContext (DbContextOptions<DiemRenLuyenMVCContext> options)
            : base(options)
        {
        }

        public DbSet<DiemRenLuyenMVC.Models.TaiKhoan> TaiKhoan { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaiKhoan>().HasData(
                // Sinh viên
                 new TaiKhoan { TenDangNhap = "22103100228", MatKhau = "123456", VaiTro = "SV" },
                 new TaiKhoan { TenDangNhap = "22103100229", MatKhau = "123456", VaiTro = "SV" },
                 new TaiKhoan { TenDangNhap = "22103100230", MatKhau = "123456", VaiTro = "SV" },
                 new TaiKhoan { TenDangNhap = "22103100231", MatKhau = "123456", VaiTro = "SV" },
                 new TaiKhoan { TenDangNhap = "22103100232", MatKhau = "123456", VaiTro = "SV" },

                // Giảng viên
                 new TaiKhoan { TenDangNhap = "22103110301", MatKhau = "123456", VaiTro = "GV" },
                 new TaiKhoan { TenDangNhap = "22103110302", MatKhau = "123456", VaiTro = "GV" },
                 new TaiKhoan { TenDangNhap = "22103110303", MatKhau = "123456", VaiTro = "GV" },

                // CTSV
                 new TaiKhoan { TenDangNhap = "22103199901", MatKhau = "admin", VaiTro = "CTSV" },
                 new TaiKhoan { TenDangNhap = "22103199902", MatKhau = "admin", VaiTro = "CTSV" }
            );
        }
    }
}
