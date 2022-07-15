using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMovieTicket.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                RapPhimDBContext db = new RapPhimDBContext();
                TaiKhoan tk = (TaiKhoan)Session["user"];
                KhachHang kh = db.KhachHang.FirstOrDefault(x => x.Email == tk.TaiKhoan1);
                ViewBag.chitieu = db.HoaDon.Where(x => x.MaKhachHang == kh.MaKhachHang && x.NgayLap.Year == DateTime.Now.Year).Sum(x => x.TongTien);
                ViewBag.PhimDangChieu = db.Phim.Where(x => x.TrangThai == 1).ToList();
                return View(kh);
            }
            return View("Error");
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return Redirect("/Home/Index");
        }
        [HttpPost]
        public JsonResult CheckSession()
        {
            JsonResult jr = new JsonResult();
            TaiKhoan tk = (TaiKhoan)Session["user"];
            if (tk != null)
            {
                jr.Data = new
                {
                    status = "OK",
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "F",
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        private String GetMD5(string txt)
        {
            String str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
        [HttpPost]
        public JsonResult checkLogin(FormCollection collection)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            string username = collection["username"];
            string password = collection["password"];
            JsonResult jr = new JsonResult();
            TaiKhoan u = db.TaiKhoan.Find(username);
            if (u == null||u.Quyen!="user") 
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                if (u.MatKhau.Trim() == GetMD5(password))
                {
                    KhachHang kh = db.KhachHang.FirstOrDefault(x => x.Email == username);
                    String ten = kh.HoTen;
                    String id = kh.MaKhachHang;
                    Session["user"] = u;
                    Session.Timeout = 15;
                    jr.Data = new
                    {
                        status = "OK",
                        name = ten,
                        id = id
                    };
                }
                else
                {
                    jr.Data = new
                    {
                        status = "F"
                    };
                }
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkLoginFb(FormCollection collection)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            string username = collection["username"];
            JsonResult jr = new JsonResult();
            TaiKhoan u = db.TaiKhoan.Find(username);
            Session["user"] = u;
            Session.Timeout = 15;
            jr.Data = new
            {
                status = "OK"
            };
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}