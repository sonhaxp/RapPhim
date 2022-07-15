using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeXemPhim.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult SearchByKey(string keyword)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return View(db.Phim.Where(x=>x.TenPhim.Contains(keyword)||x.GhiChu.Contains(keyword)).ToList());
        }
        public ActionResult Search(string keyword)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return PartialView("_Search",db.Phim.Where(x => x.TenPhim.Contains(keyword) || x.GhiChu.Contains(keyword)).ToList());
        }
    }
}