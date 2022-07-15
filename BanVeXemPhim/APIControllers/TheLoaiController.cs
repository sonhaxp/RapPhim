using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class TheLoaiController : ApiController
    {
        RapPhimDBContext db = new RapPhimDBContext();
        [HttpGet]
        public List<TheLoai> GetAllTheLoai()
        {
            return db.TheLoai.ToList();
        }
        [HttpGet]
        public TheLoai GetTheLoai(string id)
        {
            return db.TheLoai.FirstOrDefault(x=>x.MaTheLoai==id);
        }
        [HttpPost]
        public bool InsertTheLoai(string name)
        {
            TheLoai theLoai = new TheLoai();
            int s = int.Parse(db.TheLoai.OrderByDescending(x => x.MaTheLoai).FirstOrDefault().MaTheLoai.Split('L')[1]);
            s += 1;
            int t = s.ToString().Length;
            string matl = "TL";
            for (int i = 0; i < 4 - t; i++)
                matl += "0";
            matl += s;
            theLoai.MaTheLoai = matl;
            theLoai.TenTheLoai = name;
            try
            {
                db.TheLoai.Add(theLoai);
                db.SaveChanges();
            }catch(Exception)
            {
                return false;
            }
            return true;
        }
        [HttpPut]
        public bool UpdateTheLoai(string id, string name)
        {
            TheLoai theLoai = db.TheLoai.FirstOrDefault(x => x.MaTheLoai == id);
            if (theLoai == null) return false;
            theLoai.TenTheLoai = name;
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
        [HttpDelete]
        public bool deleteTheLoai(string ma)
        {
            TheLoai theLoai = db.TheLoai.FirstOrDefault(x => x.MaTheLoai == ma);
            if (theLoai == null) return false;
            try
            {
                db.TheLoai.Remove(theLoai);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
