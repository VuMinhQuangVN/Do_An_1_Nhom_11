using DiemRenLuyen.Models;
using DiemRenLuyen.Models.SinhVienModels;
using Microsoft.EntityFrameworkCore;

namespace DiemRenLuyen.Data
{
    public class DiemRenLuyenContext : DbContext
    {
        public DiemRenLuyenContext(DbContextOptions<DiemRenLuyenContext> options)
            : base(options)
        {
        }

        //Quang
        public DbSet<DiemRenLuyen.Models.TaiKhoan> TaiKhoan { get; set; } = default!;
        public DbSet<DiemRenLuyen.Models.SinhVienModels.SinhVien> SinhVien { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.PhieuDanhGia> PhieuDanhGia { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.ChiTietPhieu> ChiTietPhieu { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.MinhChung> MinhChung { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.DotDanhGia> DotDanhGias { get; set; } = default!;
        public DbSet<DiemRenLuyen.Models.SinhVienModels.TieuChi> TieuChi { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.Lop> Lop { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.Khoa> Khoa { get; set; }
        public DbSet<DiemRenLuyen.Models.SinhVienModels.GiangVien> GiangVien { get; set; }
        //Quyen
        public DbSet<DiemRenLuyen.Models.GiangVienModels.DuyetPhieu> DuyetPhieu { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChiTietPhieu>()
                .HasKey(ct => new { ct.MaPhieu, ct.MaTC });

            modelBuilder.Entity<MinhChung>()
                .HasOne(mc => mc.ChiTietPhieu)
                .WithMany(ct => ct.MinhChungs)
                .HasForeignKey(mc => new { mc.MaPhieu, mc.MaTC })
                .OnDelete(DeleteBehavior.Restrict);


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
            modelBuilder.Entity<GiangVien>()
                .ToTable("GiangVien")
                .HasData(
                new GiangVien
                {
                    MaGV = "13103100200",
                    TenGV = "Nguyễn Văn A",
                    MaKhoa = "CNTT",
                    Email = "nguyenvana@example.com",
                    DienThoai = "0901234567"
                }, new GiangVien
                {
                    MaGV = "13103100201",
                    TenGV = "Trần Thị Ngọc",
                    MaKhoa = "CNTT",
                    Email = "tranthingoc@example.com",
                    DienThoai = "0909876543"
                }

            );
            modelBuilder.Entity<Lop>()
                .ToTable("Lop")
                .HasData(
                new Lop
                {
                    MaLop = "DHTI16A1HN",
                    TenLop = "Công nghệ thông tin 1",
                    MaGV = "13103100200",
                    MaKhoa = "CNTT"
                },
                new Lop
                {
                    MaLop = "DHTI16A2HN",
                    TenLop = "Công nghệ thông tin 2",
                    MaGV = "13103100200",
                    MaKhoa = "CNTT"
                },
                new Lop
                {
                    MaLop = "DHTI16A3HN",
                    TenLop = "Công nghệ thông tin 3",
                    MaGV = "13103100201",
                    MaKhoa = "CNTT"
                }
            );
            modelBuilder.Entity<Khoa>().ToTable("Khoa").HasData(
                new Khoa
                {
                    MaKhoa = "CNTT",
                    TenKhoa = "Công nghệ thông tin",
                    MaGVTruongKhoa = "10103100001"
                }
            );
            modelBuilder.Entity<SinhVien>().ToTable("SinhVien").HasData(
                new SinhVien
                {
                    MaSV = "22103100228",
                    HoTen = "Trần Văn B",
                    NgaySinh = new DateTime(2003, 10, 4),
                    GioiTinh = true,
                    QueQuan = "Ninh Bình",
                    MaLop = "DHTI16A1HN",

                }
            );
            modelBuilder.Entity<DotDanhGia>().ToTable("DotDanhGia").HasData(
                new DotDanhGia
                {
                    MaDot = "DG001",
                    HocKy = 1,
                    NamHoc = "2024–2025",
                    ThoiGianBatDau = new DateTime(2025, 10, 1),
                    ThoiGianKetThuc = new DateTime(2025, 10, 15)
                }
            );
            modelBuilder.Entity<TieuChi>().ToTable("TieuChi").HasData(
                new TieuChi { MaTC = "TC01", TenTieuChi = "Tham gia học tập tốt", MucDiemToiDa = 10, MoTa = " " },
                new TieuChi { MaTC = "TC02", TenTieuChi = "Tích cực tham gia hoạt động lớp", MucDiemToiDa = 15, MoTa = " " }
            );

            modelBuilder.Entity<TieuChi>().HasData(
                new TieuChi { MaTC = "A", TenTieuChi = "Điểm rèn luyện mặc định của sinh viên trong học kỳ", MucDiemToiDa = 70, MoTa = "Mặc định 70 điểm" },

                new TieuChi { MaTC = "B", TenTieuChi = "Điểm rèn luyện được cộng trong kỳ (điểm cộng/lần, hoạt động...)", MucDiemToiDa = 0, MoTa = " " },

                new TieuChi { MaTC = "B1", TenTieuChi = "Điểm cộng về ý thức tham gia học tập", MucDiemToiDa = 0, MoTa = " " },

                new TieuChi { MaTC = "B11", TenTieuChi = "Đạt kết quả học tập từ 7.0 trở lên (theo thang điểm 10)", MucDiemToiDa = 0, MoTa = " " },
                new TieuChi { MaTC = "B111", TenTieuChi = "Đạt điểm trung bình học tập trên 9,0", MucDiemToiDa = 5, MoTa = " " },
                new TieuChi { MaTC = "B112", TenTieuChi = "Đạt điểm trung bình học tập từ 8,0 đến 9,0", MucDiemToiDa = 3, MoTa = " " },
                new TieuChi { MaTC = "B113", TenTieuChi = "Đạt điểm trung bình học tập từ 7,0 đến 8,0", MucDiemToiDa = 2, MoTa = " " },

                new TieuChi { MaTC = "B12", TenTieuChi = "Có tinh thần cố gắng vượt khó, vươn lên trong học tập", MucDiemToiDa = 2, MoTa = " " },
                new TieuChi { MaTC = "B13", TenTieuChi = "Tham gia các CLB học thuật, hoạt động NCKH, các cuộc thi khởi nghiệp sáng tạo", MucDiemToiDa = 100, MoTa = " " },

                new TieuChi { MaTC = "B2", TenTieuChi = "Điểm cộng về ý thức chấp hành nội quy, quy chế, quy định trong nhà trường", MucDiemToiDa = 0, MoTa = " " },
                new TieuChi { MaTC = "B21", TenTieuChi = "Có hành động tích cực trên mạng xã hội phù hợp với chủ trương của nhà trường", MucDiemToiDa = 100, MoTa = " " },

                new TieuChi { MaTC = "C", TenTieuChi = "Điểm rèn luyện bị trừ trong kỳ (điểm trừ/lần vi phạm, buổi...)", MucDiemToiDa = 0, MoTa = " " },
                new TieuChi { MaTC = "C1", TenTieuChi = "Điểm trừ về ý thức học tập", MucDiemToiDa = 0, MoTa = " " },
                new TieuChi { MaTC = "C11", TenTieuChi = "Vi phạm quy chế thi, kiểm tra", MucDiemToiDa = 4 },
                new TieuChi { MaTC = "C12", TenTieuChi = "Thi lại môn học, học phần", MucDiemToiDa = 2 },

                new TieuChi { MaTC = "C13", TenTieuChi = "Vi phạm thời gian học tập, thiếu ý thức trong học tập", MucDiemToiDa = 0, MoTa = " " },
                new TieuChi { MaTC = "C131", TenTieuChi = "Nghỉ học không có lý do", MucDiemToiDa = 100, MoTa = " " },
                new TieuChi { MaTC = "C132", TenTieuChi = "Đi học muộn, trốn tiết, bỏ giờ,..", MucDiemToiDa = 100, MoTa = " " },

                new TieuChi { MaTC = "C2", TenTieuChi = "Điểm trừ về ý thức chấp hành nội quy, quy chế nhà trường", MucDiemToiDa = 0, MoTa = " " },
                new TieuChi { MaTC = "C21", TenTieuChi = "Đi học hộ hoặc nhờ người khác đi học hộ", MucDiemToiDa = 100, MoTa = " " },
                new TieuChi { MaTC = "C22", TenTieuChi = "Không đóng học phí đúng thời hạn", MucDiemToiDa = 5, MoTa = " " }
            );

        }
    }
}
