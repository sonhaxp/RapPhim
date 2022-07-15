using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class ThanhVienController : ApiController
    {
        public KhachHang GetKhachHangById(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.KhachHang.FirstOrDefault(x => x.MaKhachHang == ma);
        }
        [HttpGet]
        public KhachHang GetKhachHang(string username)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.KhachHang.FirstOrDefault(x=>x.Email==username);
        }
        [HttpGet]
        public List<KhachHang> GetListKhachHang()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.KhachHang.ToList();
        }
        [HttpPost]
        public bool AddKhachHang(string hoten,string sdt,string email,string diachi,string ngaysinh,int gioitinh)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.KhachHang.OrderByDescending(x => x.MaKhachHang).FirstOrDefault().MaKhachHang.Split('H')[1]);
            s += 1;
            int t = s.ToString().Length;
            string makh = "KH";
            for (int i = 0; i < 4-t; i++)
                makh += "0";
            makh += s;
            KhachHang khach = new KhachHang();
            khach.MaKhachHang = makh;
            khach.HoTen = hoten;
            khach.SoDienThoai = sdt;
            khach.Email = email;
            khach.DiaChi = diachi;
            string []date1 = ngaysinh.Split(' ');
            string[] date = date1[0].Split('-');
            khach.NgaySinh = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
            khach.GioiTinh = gioitinh==1?"Nam":"Nữ";
            try
            {
                db.KhachHang.Add(khach);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
            
        }
        [HttpPut]
        public bool UpdateKhachHang(string ma,string hoten, string sdt, string email, string diachi, string ngaysinh, int gioitinh)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            KhachHang khach = db.KhachHang.FirstOrDefault(x => x.MaKhachHang == ma);
            if (khach == null) return false;
            try {
                TaiKhoan tk = db.TaiKhoan.FirstOrDefault(x => x.TaiKhoan1 == khach.Email);
                TaiKhoan tk1 = new TaiKhoan();
                tk1.TaiKhoan1 = email;
                tk1.MatKhau = tk.MatKhau;
                tk1.Quyen = tk.Quyen;
                db.TaiKhoan.Remove(tk);
                db.TaiKhoan.Add(tk1);
                db.SaveChanges();
            } catch (Exception)
            {
                return false;
            }
            khach.HoTen = hoten;
            khach.SoDienThoai = sdt;
            
            khach.Email = email;
            khach.DiaChi = diachi;
            string[] date1 = ngaysinh.Split(' ');
            string[] date = date1[0].Split('-');
            khach.NgaySinh = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
            khach.GioiTinh = gioitinh == 1 ? "Nam" : "Nữ";
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateDiachiKhachHang(string email, string diachi)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            KhachHang khach = db.KhachHang.FirstOrDefault(x => x.Email == email);
            khach.DiaChi = diachi;
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool DeleteKhachHang(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            KhachHang khach = db.KhachHang.FirstOrDefault(x => x.MaKhachHang == ma);
            try
            {
                db.KhachHang.Remove(khach);
                db.TaiKhoan.Remove(db.TaiKhoan.FirstOrDefault(x => x.TaiKhoan1 == khach.Email));
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        [HttpGet]
        public List<KhachHang> GetKH(int page)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            var qr = db.KhachHang.OrderBy(x => x.MaKhachHang).Skip((page - 1) * 10).Take(10);
            return qr.ToList();
        }
        [HttpGet]
        public List<KhachHang> GetListKH(int sl)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            var qr = db.KhachHang.Take(10);
            return qr.ToList();
        }
    }
}
