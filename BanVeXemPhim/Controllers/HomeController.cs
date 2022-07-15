using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMovieTicket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderMovie()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            ViewBag.PhimSapChieu = db.Phim.Where(x => x.TrangThai == 2).ToList();
            return PartialView("_Main",db.Phim.Where(x => x.TrangThai == 1).ToList());
        }
        public ActionResult RenderNav()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            ViewBag.PhimSapChieu = db.Phim.Where(x => x.TrangThai == 2).Take(3).ToList();
            return PartialView("_Header_Nav", db.Phim.Where(x => x.TrangThai == 1).Take(3).ToList());
        }
    }
}