using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class PhimController : ApiController
    {
        [HttpGet]
        [Route("all-film")]
        public List<Phim2> GetAllPhim()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.Phim2.Where(x => x.TrangThai == 1).ToList();
        }
        [HttpGet]
        public List<Phim2> GetAllPhim2 ()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.Phim2.ToList();
        }
        [HttpGet]
        public List<Phim2> GetPagePhim(int page)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            var qr = db.Phim2.OrderBy(x=>x.MaPhim).Skip((page - 1) * 10).Take(10);
            return qr.ToList();
        }
        [HttpPost]
        public bool AddPhim(string ten, int thoiluong, DateTime ngaycongchieu, string ngonngu, string dienvien, string quocgia, string nsx, string tomtat, int trangthai, string matheloai, HttpPostedFileBase anh, string ghichu, int tuoi)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.Phim.OrderByDescending(x => x.MaPhim).FirstOrDefault().MaPhim.Split('P')[1]);
            s += 1;
            int t = s.ToString().Length;
            string maphim = "P";
            for (int i = 0; i < 4 - t; i++)
                maphim += "0";
            maphim += s;
            Phim phim = new Phim();
            phim.MaPhim = maphim;
            phim.TenPhim = ten;
            phim.ThoiLuong = thoiluong;
            phim.NgayCongChieu = ngaycongchieu;
            phim.NgonNgu = ngonngu;
            phim.DienVien = dienvien;
            phim.QuocGia = quocgia;
            phim.NhaSanXuat = nsx;
            phim.TomTat = tomtat;
            phim.TrangThai = trangthai;
            phim.MaTheLoai = matheloai;
            phim.Anh = anh.FileName;
            phim.GhiChu = ghichu;
            phim.Tuoi = tuoi;
            string path = Path.Combine("/Content/img", anh.FileName);
            anh.SaveAs(path);
            try
            {
                db.Phim.Add(phim);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdatePhim(string ma, string ten, int thoiluong, DateTime ngaycongchieu, string ngonngu, string dienvien, string quocgia, string nsx, string tomtat, int trangthai, string matheloai, string anh, string ghichu, int tuoi)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            Phim phim = db.Phim.FirstOrDefault(x => x.MaPhim == ma);
            if (phim == null) return false;
            phim.TenPhim = ten;
            phim.ThoiLuong = thoiluong;
            phim.NgayCongChieu = ngaycongchieu;
            phim.NgonNgu = ngonngu;
            phim.DienVien = dienvien;
            phim.QuocGia = quocgia;
            phim.NhaSanXuat = nsx;
            phim.TomTat = tomtat;
            phim.TrangThai = trangthai;
            phim.MaTheLoai = matheloai;
            phim.Anh = anh;
            phim.GhiChu = ghichu;
            phim.Tuoi = tuoi;
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
        [HttpDelete]
        public bool deletePhim(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            Phim phim = db.Phim.FirstOrDefault(x => x.MaPhim == ma);
            List<SuatChieu> sc = db.SuatChieu.Where(x => x.MaPhim == ma).ToList();
            foreach(var i in sc)
            {
                db.VeBan.RemoveRange(db.VeBan.Where(x => x.MaSuatChieu == i.MaSuatChieu));
            }
            db.SaveChanges();
            db.SuatChieu.RemoveRange(sc);
            db.SaveChanges();
            db.Phim.Remove(phim);
            db.SaveChanges();
            return true;
        }
    }
}
