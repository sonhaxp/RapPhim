using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMovieTicket.Controllers
{
    public class Choose_TicketController : Controller
    {
        // GET: Choose_Ticket
        public ActionResult Index(string id)
        {
            if (Session["user"] == null) return View("Error");
            RapPhimDBContext db = new RapPhimDBContext();
            SuatChieu2 suat = db.SuatChieu2.FirstOrDefault(x => x.MaSuatChieu == id);
            List<DoAn> doan = db.DoAn.ToList();
            Session["suat"] = suat;
            ViewBag.doan = doan;
            PhongChieu phong = db.PhongChieu.FirstOrDefault(x => x.MaPhongChieu == suat.MaPhongChieu);
            List<VeBan> ve = db.VeBan.Where(x => x.MaSuatChieu == suat.MaSuatChieu).ToList();
            List<GheNgoi> ghe = db.GheNgoi.Where(x=>x.MaPhongChieu==suat.MaPhongChieu).OrderBy(x=>x.ViTriDay).ThenBy(x=>x.ViTriCot).ToList();
            ViewBag.ghe = ghe;
            ViewBag.ve = ve;
            ViewBag.phong = phong;
            return View(suat);
        }
        public JsonResult ShowMember()
        {
            JsonResult jr = new JsonResult();
            TaiKhoan tk = (TaiKhoan)Session["user"];
            if (tk != null)
            {
                RapPhimDBContext db = new RapPhimDBContext();
                KhachHang kh = db.KhachHang.FirstOrDefault(x => x.Email == tk.TaiKhoan1);
                jr.Data = new
                {
                    status = "OK",
                    name = kh.HoTen,
                    sdt = kh.SoDienThoai,
                    email = kh.Email
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
        public JsonResult LapHD(FormCollection collection)
        {
            string maghe = collection["maghe"];
            char[] c = new char[] { ',',' ',',' };
            string []ghengoiCL = collection["ghengoi"].Split(c, StringSplitOptions.RemoveEmptyEntries);
            string ghengoi = "";
            for (int i = 0; i < ghengoiCL.Length-1; i++)
            {
                ghengoi += ghengoiCL[i] + ", ";
            }
            ghengoi += ghengoiCL[ghengoiCL.Length - 1];
            string[] ghe = maghe.Split(',');
            string doan = collection["doan"];
            string[] doan1 = doan.Split(',');
            string tendoan = collection["tendoan"];
            long tongtien = long.Parse(collection["tongtien"]);
            JsonResult jr = new JsonResult();
            TaiKhoan tk = (TaiKhoan)Session["user"];
            SuatChieu2 sc = (SuatChieu2)Session["suat"];
            if (tk != null)
            {
                try
                {
                    RapPhimDBContext db = new RapPhimDBContext();
                    KhachHang kh = db.KhachHang.FirstOrDefault(x => x.Email == tk.TaiKhoan1);
                    HoaDon hoadon = new HoaDon();
                    hoadon.MaHoaDon = AutoMaHD();
                    hoadon.MaKhachHang = kh.MaKhachHang;
                    hoadon.NgayLap = DateTime.Now;
                    hoadon.Ghe = ghengoi;
                    hoadon.DoAn = tendoan;
                    hoadon.TongTien = tongtien;
                    hoadon.SoVe = ghe.Length;
                    hoadon.Rap = sc.TenRap;
                    hoadon.Phim = sc.TenPhim;
                    string[] day = { "Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy" };
                    string s = sc.ThoiGianChieu.ToString().Split(' ')[0];
                    var ngay = sc.GioChieu + " | " + day[(int)sc.ThoiGianChieu.Value.DayOfWeek] + ", " + s;
                    hoadon.SuatChieu = ngay;
                    db.HoaDon.Add(hoadon);
                    db.SaveChanges();
                    List<DoAn> doAns = db.DoAn.ToList();
                    for(int i = 0; i < doan1.Length; i++)
                    {
                        if (int.Parse(doan1[i]) > 0)
                        {
                            CTHoaDon cthd = new CTHoaDon();
                            cthd.MaHoaDon = hoadon.MaHoaDon;
                            cthd.MaDoAn = doAns[i].MaDoAn;
                            cthd.SoLuong = int.Parse(doan1[0]);
                            cthd.ThanhTien = cthd.SoLuong * doAns[i].GiaBan;
                            db.CTHoaDon.Add(cthd);
                            db.SaveChanges();
                        }
                    }
                    //if (int.Parse(doan1[0])>0)
                    //{
                    //    CTHoaDon cthd = new CTHoaDon();
                    //    cthd.MaHoaDon = hoadon.MaHoaDon;
                    //    cthd.MaDoAn = "DA0001";
                    //    cthd.SoLuong = int.Parse(doan1[0]);
                    //    cthd.ThanhTien = cthd.SoLuong * 199000;
                    //    db.CTHoaDon.Add(cthd);
                    //    db.SaveChanges();
                    //}
                    //if (int.Parse(doan1[1]) > 0)
                    //{
                    //    CTHoaDon cthd = new CTHoaDon();
                    //    cthd.MaHoaDon = hoadon.MaHoaDon;
                    //    cthd.MaDoAn = "DA0002";
                    //    cthd.SoLuong = int.Parse(doan1[1]);
                    //    cthd.ThanhTien = cthd.SoLuong * 92000;
                    //    db.CTHoaDon.Add(cthd);
                    //    db.SaveChanges();
                    //}
                    //if (int.Parse(doan1[2]) > 0)
                    //{
                    //    CTHoaDon cthd = new CTHoaDon();
                    //    cthd.MaHoaDon = hoadon.MaHoaDon;
                    //    cthd.MaDoAn = "DA0003";
                    //    cthd.SoLuong = int.Parse(doan1[2]);
                    //    cthd.ThanhTien = cthd.SoLuong * 79000;
                    //    db.CTHoaDon.Add(cthd);
                    //    db.SaveChanges();
                    //}
                    foreach(var i in ghe)
                    {
                        VeBan ve = new VeBan();
                        ve.MaVe = AutoMaVe();
                        ve.MaSuatChieu = sc.MaSuatChieu;
                        ve.MaGheNgoi = i;
                        ve.MaHoaDon = hoadon.MaHoaDon;
                        db.VeBan.Add(ve);
                        db.SaveChanges();
                    }
                    jr.Data = new
                        {
                            status = "OK",
                        };
                }
                catch (Exception)
                {
                    jr.Data = new
                    { 
                    status = "F",
                    };
                }
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
        public string AutoMaHD()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.HoaDon.OrderByDescending(x => x.MaHoaDon).FirstOrDefault().MaHoaDon.Split('D')[1]);
            s += 1;
            int t = s.ToString().Length;
            string mahd = "HD";
            for (int i = 0; i < 4 - t; i++)
                mahd += "0";
            mahd += s;
            return mahd;
        }
        public string AutoMaVe()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            int s = int.Parse(db.VeBan.OrderByDescending(x => x.MaVe).FirstOrDefault().MaVe.Split('V')[1]);
            s += 1;
            int t = s.ToString().Length;
            string mave = "V";
            for (int i = 0; i < 4 - t; i++)
                mave += "0";
            mave += s;
            return mave;
        }
    }
}