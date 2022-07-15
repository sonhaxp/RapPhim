using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeXemPhim.Controllers
{
    public class InfoPhimController : Controller
    {
        // GET: InfoPhim
        public ActionResult Index(string id)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            ViewBag.PhimDangChieu = db.Phim.Where(x => x.TrangThai == 1).Take(3).ToList();
            return View(db.Phim2.FirstOrDefault(x=>x.MaPhim==id));
        }
    }
}