using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class SuatChieuController : ApiController
    {
        bool checkGio(string gio)
        {
            var a = gio.Split(':');
            int h = int.Parse(a[0]);
            int m = int.Parse(a[1]);
            return h*60+m-60>DateTime.Now.Hour*60+DateTime.Now.Minute;
        }
        bool checkNgay(DateTime dt,string gio)
        {
            if (dt.Year < DateTime.Now.Year) return false;
            else if (dt.Year > DateTime.Now.Year) return true;
            else
            {
                if (dt.Month > DateTime.Now.Month) return true;
                else if (dt.Month < DateTime.Now.Month) return false;
                else
                {
                    if (dt.Day > DateTime.Now.Day) return true;
                    else if (dt.Day < DateTime.Now.Day) return false;
                    else
                    {
                        return checkGio(gio);
                    }    
                }
            }
        }
        [HttpGet]
        public List<SuatChieu2> GetSuatChieuByRap_Phim(string marap,string maphim)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            List<SuatChieu2> suat = db.SuatChieu2.OrderBy(x=>x.ThoiGianChieu).Where(x => x.MaRap == marap && x.MaPhim == maphim).ToList();
            for(int i = 0;i<suat.Count;i++)
            {
                if (checkNgay(suat[i].ThoiGianChieu.Value, suat[i].GioChieu) == false)
                {
                    suat.Remove(suat[i--]);
                }
            }
            return suat;
        }
        [HttpGet]
        public List<SuatChieu2> GetListSuat()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.SuatChieu2.ToList();
        }
        [HttpGet]
        public SuatChieu2 GetSuat(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.SuatChieu2.FirstOrDefault(x=>x.MaSuatChieu==ma);
        }
        [HttpPost]
        public bool AddSuatChieu(DateTime thoigianchieu, string maphim, string giochieu, string maphongchieu)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.SuatChieu.OrderByDescending(x => x.MaSuatChieu).FirstOrDefault().MaSuatChieu.Split('C')[1]);
            s += 1;
            int t = s.ToString().Length;
            string masuat = "SC";
            for (int i = 0; i < 4 - t; i++)
                masuat += "0";
            masuat += s;
            SuatChieu sc = new SuatChieu();
            sc.MaSuatChieu = masuat;
            sc.ThoiGianChieu = thoigianchieu;
            sc.GioChieu = giochieu;
            sc.MaPhim = maphim;
            sc.MaPhongChieu = maphongchieu;
            try
            {
                db.SuatChieu.Add(sc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateSuat(string ma, DateTime thoigianchieu, string maphim, string giochieu, string maphongchieu)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            SuatChieu sc = db.SuatChieu.FirstOrDefault(x => x.MaSuatChieu == ma);
            sc.ThoiGianChieu = thoigianchieu;
            sc.GioChieu = giochieu;
            sc.MaPhim = maphim;
            sc.MaPhongChieu = maphongchieu;
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
        public bool deleteSuat(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            db.VeBan.RemoveRange(db.VeBan.Where(x => x.MaSuatChieu == ma));
            db.SaveChanges();
            db.SuatChieu.Remove(db.SuatChieu.FirstOrDefault(x => x.MaSuatChieu == ma));
            db.SaveChanges();
            return true;
        }
    }
}
