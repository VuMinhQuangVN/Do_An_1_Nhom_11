using DiemRenLuyen.Models;
using Microsoft.EntityFrameworkCore;

namespace DiemRenLuyen.Data
{
    public class DiemRenLuyenContext : DbContext
    {
        public DiemRenLuyenContext(DbContextOptions<DiemRenLuyenContext> options)
            : base(options)
        {
        }

        public DbSet<DiemRenLuyen.Models.TaiKhoan> TaiKhoan { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaiKhoan>()
                .ToTable("TaiKhoan")
                .HasData(
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
