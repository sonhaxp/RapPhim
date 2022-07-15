using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class GheController : ApiController
    {
        [HttpGet]
        public List<GheNgoi> GetListGheBySC(string sc)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.GheNgoi.Where(x => x.MaPhongChieu == sc).OrderBy(x => x.ViTriDay).OrderBy(x => x.ViTriCot).ToList();
        }
        [HttpPost]
        public Boolean UpdateGhe(string ma,int tt)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            GheNgoi ghe = db.GheNgoi.FirstOrDefault(x => x.MaGheNgoi == ma);
            ghe.TrangThai = tt;
            db.SaveChanges();
            return true;
        }
    }
}
