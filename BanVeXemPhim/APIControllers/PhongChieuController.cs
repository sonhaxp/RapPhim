using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class PhongChieuController : ApiController
    {
        [HttpGet]
        public List<PhongChieu2> GetListPhongChieu()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.PhongChieu2.ToList();
        }
        [HttpGet]
        public List<PhongChieu> GetListPhongChieuByRap(string marap)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.PhongChieu.Where(x=>x.MaRap==marap).ToList();
        }
        [HttpGet]
        public PhongChieu GetListPhongChieu(string id)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.PhongChieu.FirstOrDefault(x=>x.MaPhongChieu==id);
        }
        [HttpPost]
        public bool AddPhongChieu(string ten, int hang,int cot, string marap)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.PhongChieu.OrderByDescending(x => x.MaPhongChieu).FirstOrDefault().MaPhongChieu.Split('C')[1]);
            s += 1;
            int t = s.ToString().Length;
            string maphong = "PC";
            for (int i = 0; i < 4 - t; i++)
                maphong += "0";
            maphong += s;
            PhongChieu pc = new PhongChieu();
            pc.MaPhongChieu = maphong;
            pc.TenPhong = ten;
            pc.soluonghang = hang;
            pc.soluongcot = cot;
            pc.MaRap = marap;
            try
            {
                db.PhongChieu.Add(pc);
                db.SaveChanges();
                int k = int.Parse(db.GheNgoi.OrderByDescending(x => x.MaPhongChieu).ThenByDescending(x => x.ViTriDay).ThenByDescending(x => x.ViTriCot).FirstOrDefault().MaGheNgoi.Split('G')[1]);
                    k += 1;
                    string[] hang1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                    for (int r = 1; r <= hang; r++)
                    {
                        for (int c = 1; c <= cot; c++)
                        {
                            GheNgoi ghe = new GheNgoi();
                            ghe.MaGheNgoi = "G" + k;
                            ghe.ViTriDay = hang1[r];
                            ghe.ViTriCot = c;
                            ghe.TrangThai = 0;
                            ghe.MaPhongChieu = maphong;
                            db.GheNgoi.Add(ghe);
                            k++;
                        }
                    }
                    db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdatePhong(string ma, string ten, int hang, int cot, string marap)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            PhongChieu pc = db.PhongChieu.FirstOrDefault(x=>x.MaPhongChieu==ma);
            pc.TenPhong = ten;
            pc.soluonghang = hang;
            pc.soluongcot = cot;
            pc.MaRap = marap;
            try
            {
                db.SaveChanges();
                db.GheNgoi.RemoveRange(db.GheNgoi.Where(x => x.MaPhongChieu == ma));
                int k = int.Parse(db.GheNgoi.OrderByDescending(x => x.MaPhongChieu).ThenByDescending(x => x.ViTriDay).ThenByDescending(x => x.ViTriCot).FirstOrDefault().MaGheNgoi.Split('G')[1]);
                k += 1;
                string[] hang1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                for (int r = 1; r <= hang; r++)
                {
                    for (int c = 1; c <= cot; c++)
                    {
                        GheNgoi ghe = new GheNgoi();
                        ghe.MaGheNgoi = "G" + k;
                        ghe.ViTriDay = hang1[r];
                        ghe.ViTriCot = c;
                        ghe.TrangThai = 0;
                        ghe.MaPhongChieu = ma;
                        db.GheNgoi.Add(ghe);
                        k++;
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete]
        public bool deletePhong(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            List<SuatChieu> sc = db.SuatChieu.Where(x => x.MaPhongChieu == ma).ToList();
            foreach (var i in sc)
            {
                db.VeBan.RemoveRange(db.VeBan.Where(x => x.MaSuatChieu == i.MaSuatChieu));
            }
            db.SaveChanges();
            db.SuatChieu.RemoveRange(sc);
            db.SaveChanges();
            db.GheNgoi.RemoveRange(db.GheNgoi.Where(x => x.MaPhongChieu == ma));
            db.SaveChanges();
            return true;
        }
    }

}
