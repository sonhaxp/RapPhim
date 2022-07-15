using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class RapController : ApiController
    {
        [HttpGet]
        public List<Rap> GetListRap()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.Rap.ToList();
        }
        
        [HttpPost]
        public bool AddRap(string ten, string diachi)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.Rap.OrderByDescending(x => x.MaRap).FirstOrDefault().MaRap.Split('R')[1]);
            s += 1;
            int t = s.ToString().Length;
            string marap = "R";
            for (int i = 0; i < 4 - t; i++)
                marap += "0";
            marap += s;
            Rap rap = new Rap();
            rap.MaRap = marap;
            rap.DiaChi = diachi;
            rap.TenRap = ten;
            try
            {
                db.Rap.Add(rap);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateRap(string ma, string ten, string diachi)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            Rap rap = db.Rap.FirstOrDefault(x => x.MaRap == ma);
            if (rap == null) return false;
            rap.TenRap = ten;
            rap.DiaChi = diachi;
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
        public bool DeleteRap(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            try
            {
                List<PhongChieu> pc = db.PhongChieu.Where(x => x.MaRap == ma).ToList();
                List<SuatChieu> sc = new List<SuatChieu>();
                foreach (var i in pc)
                {
                    sc.AddRange(db.SuatChieu.Where(x => x.MaPhongChieu == ma).ToList());
                }
                foreach (var i in sc)
                {
                    db.VeBan.RemoveRange(db.VeBan.Where(x => x.MaSuatChieu == i.MaSuatChieu));
                }
                db.SaveChanges();
                db.SuatChieu.RemoveRange(sc);
                db.SaveChanges();
                foreach (var i in pc)
                {
                    db.GheNgoi.RemoveRange(db.GheNgoi.Where(x => x.MaPhongChieu == i.MaPhongChieu));
                }
                db.SaveChanges();
                db.Rap.Remove(db.Rap.FirstOrDefault(x => x.MaRap == ma));
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpGet]
        public List<Rap> GetRapByPhim(string id) 
        {
            RapPhimDBContext db = new RapPhimDBContext();
            List<SuatChieu2> suat = db.SuatChieu2.Where(x => x.MaPhim == id).ToList();
            List<Rap> rap = new List<Rap>();
            HashSet<string> h = new HashSet<string>();
            foreach(SuatChieu2 i in suat)
            {
                h.Add(i.MaRap);
            }
            foreach (string i in h)
            {
                rap.Add(db.Rap.FirstOrDefault(x=>x.MaRap==i));
            }
            return rap;
        }
        [HttpGet]
        public Rap GetRap(string marap)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.Rap.FirstOrDefault(x => x.MaRap == marap);
        }
    }
}
