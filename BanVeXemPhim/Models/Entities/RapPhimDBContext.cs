using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BanVeXemPhim.Models.Entities
{
    public partial class RapPhimDBContext : DbContext
    {
        public RapPhimDBContext()
            : base("name=RapPhimDBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<CTHoaDon> CTHoaDon { get; set; }
        public virtual DbSet<DoAn> DoAn { get; set; }
        public virtual DbSet<GheNgoi> GheNgoi { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<Phim> Phim { get; set; }
        public virtual DbSet<PhongChieu> PhongChieu { get; set; }
        public virtual DbSet<Rap> Rap { get; set; }
        public virtual DbSet<SuatChieu> SuatChieu { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }
        public virtual DbSet<VeBan> VeBan { get; set; }
        public virtual DbSet<ChitieuKH> ChitieuKH { get; set; }
        public virtual DbSet<Phim2> Phim2 { get; set; }
        public virtual DbSet<PhongChieu2> PhongChieu2 { get; set; }
        public virtual DbSet<SuatChieu2> SuatChieu2 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTHoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<CTHoaDon>()
                .Property(e => e.MaDoAn)
                .IsUnicode(false);

            modelBuilder.Entity<DoAn>()
                .Property(e => e.MaDoAn)
                .IsUnicode(false);

            modelBuilder.Entity<DoAn>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<DoAn>()
                .HasMany(e => e.CTHoaDon)
                .WithRequired(e => e.DoAn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GheNgoi>()
                .Property(e => e.MaGheNgoi)
                .IsUnicode(false);

            modelBuilder.Entity<GheNgoi>()
                .Property(e => e.ViTriDay)
                .IsUnicode(false);

            modelBuilder.Entity<GheNgoi>()
                .Property(e => e.MaPhongChieu)
                .IsUnicode(false);

            modelBuilder.Entity<GheNgoi>()
                .HasMany(e => e.VeBan)
                .WithRequired(e => e.GheNgoi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.Ghe)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CTHoaDon)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDon)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaPhim)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaTheLoai)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .HasMany(e => e.SuatChieu)
                .WithRequired(e => e.Phim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongChieu>()
                .Property(e => e.MaPhongChieu)
                .IsUnicode(false);

            modelBuilder.Entity<PhongChieu>()
                .Property(e => e.MaRap)
                .IsUnicode(false);

            modelBuilder.Entity<PhongChieu>()
                .HasMany(e => e.GheNgoi)
                .WithRequired(e => e.PhongChieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongChieu>()
                .HasMany(e => e.SuatChieu)
                .WithRequired(e => e.PhongChieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rap>()
                .Property(e => e.MaRap)
                .IsUnicode(false);

            modelBuilder.Entity<Rap>()
                .HasMany(e => e.PhongChieu)
                .WithRequired(e => e.Rap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SuatChieu>()
                .Property(e => e.MaSuatChieu)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu>()
                .Property(e => e.MaPhim)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu>()
                .Property(e => e.MaPhongChieu)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu>()
                .Property(e => e.GioChieu)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu>()
                .HasMany(e => e.VeBan)
                .WithRequired(e => e.SuatChieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TaiKhoan1)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.MaTheLoai)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.Phim)
                .WithRequired(e => e.TheLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VeBan>()
                .Property(e => e.MaVe)
                .IsUnicode(false);

            modelBuilder.Entity<VeBan>()
                .Property(e => e.MaSuatChieu)
                .IsUnicode(false);

            modelBuilder.Entity<VeBan>()
                .Property(e => e.MaGheNgoi)
                .IsUnicode(false);

            modelBuilder.Entity<VeBan>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<ChitieuKH>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<ChitieuKH>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Phim2>()
                .Property(e => e.MaPhim)
                .IsUnicode(false);

            modelBuilder.Entity<Phim2>()
                .Property(e => e.MaTheLoai)
                .IsUnicode(false);

            modelBuilder.Entity<Phim2>()
                .Property(e => e.Anh)
                .IsUnicode(false);

            modelBuilder.Entity<PhongChieu2>()
                .Property(e => e.MaPhongChieu)
                .IsUnicode(false);

            modelBuilder.Entity<PhongChieu2>()
                .Property(e => e.MaRap)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu2>()
                .Property(e => e.MaSuatChieu)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu2>()
                .Property(e => e.MaPhim)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu2>()
                .Property(e => e.MaPhongChieu)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu2>()
                .Property(e => e.GioChieu)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu2>()
                .Property(e => e.MaRap)
                .IsUnicode(false);

            modelBuilder.Entity<SuatChieu2>()
                .Property(e => e.Anh)
                .IsUnicode(false);
        }
    }
}
