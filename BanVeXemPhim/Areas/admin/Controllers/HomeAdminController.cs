using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeXemPhim.Areas.admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: admin/Home
        public ActionResult Index()
        {
            TaiKhoan tk = (TaiKhoan)Session["admin"];
            if (tk == null) return View("_Login");
            return View(tk);
        }
        public JsonResult GetAcc()
        {
            TaiKhoan tk = (TaiKhoan)Session["admin"];
            JsonResult jr = new JsonResult();
            if (tk == null)
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "OK",
                    name = tk.TaiKhoan1
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkLogin(FormCollection collection)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            string username = collection["username"];
            string password = collection["password"];
            JsonResult jr = new JsonResult();
            TaiKhoan u = db.TaiKhoan.FirstOrDefault(x=>x.TaiKhoan1==username);
            if (u == null)
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                if (u.MatKhau.Trim() == GetMD5(password)&&u.Quyen=="admin")
                {
                    Session["admin"] = u;
                    jr.Data = new
                    {
                        status = "OK"
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
        public ActionResult RapPhim()
        {
            return PartialView("_Rap");
        }
        public ActionResult TrangChu()
        {
            return PartialView("_Content");
        }
        public ActionResult PhongChieu()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return PartialView("_PhongChieu",db.Rap.ToList());
        }
        public ActionResult SuatChieu()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            ViewBag.Phim = db.Phim.ToList();
            ViewBag.Phong = db.PhongChieu.ToList();
            ViewBag.Rap = db.Rap.ToList();
            return PartialView("_SuatChieu");
        }
        public ActionResult KhachHang()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return PartialView("_KhachHang", (int)Math.Ceiling((double)db.KhachHang.ToList().Count / 10));
        }
        public ActionResult DoAn()
        {
            return PartialView("_DoAn");
        }
        public ActionResult TheLoai()
        {
            return PartialView("_TheLoai");
        }
        public ActionResult Phim()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return PartialView("_Phim", (int)Math.Ceiling((double)db.Phim2.ToList().Count / 10));
        }
        [HttpPost]
        public ActionResult uploadPhim(string ten, int thoiluong, DateTime ngaycongchieu, string ngonngu, string dienvien, string quocgia, string nsx, string tomtat, int trangthai, string matheloai, HttpPostedFileBase anh, string ghichu, int tuoi)
        {
            Phim phim = new Phim();
            RapPhimDBContext context = new RapPhimDBContext();
            int s = int.Parse(context.Phim.OrderByDescending(x => x.MaPhim).FirstOrDefault().MaPhim.Split('P')[1]);
            s += 1;
            int t = s.ToString().Length;
            string maphim = "P";
            for (int i = 0; i < 4 - t; i++)
                maphim += "0";
            maphim += s;
            phim.MaPhim = maphim;
            phim.TenPhim = ten;
            phim.ThoiLuong = thoiluong;
            phim.NgayCongChieu = ngaycongchieu;
            phim.NgonNgu = ngonngu;
            phim.DienVien = dienvien;
            phim.QuocGia = quocgia;
            phim.NhaSanXuat = nsx;
            phim.TomTat = tomtat;
            phim.TrangThai = trangthai;
            phim.MaTheLoai = matheloai;
            phim.Anh = anh.FileName;
            phim.GhiChu = ghichu;
            phim.Tuoi = tuoi;
            if (anh.ContentLength > 0)
            {
                context.Phim.Add(phim);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        message = "error"
                    });
                }
                string filePath = Path.Combine(Server.MapPath("~/Content/img"), anh.FileName);
                anh.SaveAs(filePath);
                return Json(new
                {
                    message = "ok"
                });
            }
            return Json(new
            {
                message = "error"
            });
        }
        [HttpGet]
        public ActionResult getPhimId(string id)
        {
            RapPhimDBContext context = new RapPhimDBContext();
            var qr = from p in context.Phim
                     where p.MaPhim == id
                     select p;
            qr.SingleOrDefault();
            return Json(new
            {
                data = qr
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult updatePhim(string id, string ten, int thoiluong, DateTime ngaycongchieu, string ngonngu, string dienvien, string quocgia, string nsx, string tomtat, int trangthai, string matheloai, HttpPostedFileBase anh, string ghichu, int tuoi)
        {
            RapPhimDBContext context = new RapPhimDBContext();
            Phim phim = (from p in context.Phim
                         where p.MaPhim == id
                         select p).FirstOrDefault();
            if (phim != null && anh.ContentLength > 0)
            {
                phim.TenPhim = ten;
                phim.ThoiLuong = thoiluong;
                phim.NgayCongChieu = ngaycongchieu;
                phim.NgonNgu = ngonngu;
                phim.DienVien = dienvien;
                phim.QuocGia = quocgia;
                phim.NhaSanXuat = nsx;
                phim.TomTat = tomtat;
                phim.TrangThai = trangthai;
                phim.MaTheLoai = matheloai;
                phim.Anh = anh.FileName;
                phim.GhiChu = ghichu;
                phim.Tuoi = tuoi;

                context.SaveChanges();

                string filePath = Path.Combine(Server.MapPath("~/Content/img"), anh.FileName);
                anh.SaveAs(filePath);
                return Json(new
                {
                    data = id,
                    message = "ok"
                });
            }

            return Json(new
            {
                data = id,
                anh = anh.ContentLength
            });
        }
    }
}