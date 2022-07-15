using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class DoAnController : ApiController
    {
        [HttpGet]
        public List<DoAn> GetListDoAn()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.DoAn.ToList();
        }
        [HttpGet]
        public DoAn GetDoAn(string id)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.DoAn.FirstOrDefault(x=>x.MaDoAn==id);
        }
        [HttpPost]
        public bool AddDoAn(string ten,long gia,string anh,string chitiet)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.DoAn.OrderByDescending(x => x.MaDoAn).FirstOrDefault().MaDoAn.Split('A')[1]);
            s += 1;
            int t = s.ToString().Length;
            string mada = "DA";
            for (int i = 0; i < 4 - t; i++)
                mada += "0";
            mada += s;
            DoAn da = new DoAn();
            da.MaDoAn = mada;
            da.TenDoAn = ten;
            da.GiaBan = gia;
            da.Anh = anh;
            da.ChiTiet = chitiet;
            try
            {
                db.DoAn.Add(da);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateDoAn(string ma,string ten, long gia, string anh, string chitiet)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            DoAn da = db.DoAn.FirstOrDefault(x=>x.MaDoAn==ma);
            if (da == null) return false;
            da.TenDoAn = ten;
            da.GiaBan = gia;
            da.Anh = anh;
            da.ChiTiet = chitiet;
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
        public bool DeleteDoAn(string ma)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            try
            {
                db.CTHoaDon.RemoveRange(db.CTHoaDon.Where(x => x.MaDoAn == ma).ToList());
                db.SaveChanges();
                try
                {
                    db.DoAn.Remove(db.DoAn.FirstOrDefault(x => x.MaDoAn == ma));
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
