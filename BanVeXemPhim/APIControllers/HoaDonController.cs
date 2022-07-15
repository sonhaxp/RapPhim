using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanVeXemPhim.APIControllers
{
    public class HoaDonController : ApiController
    {
        [HttpGet]
        public List<HoaDon> GetListHD(string makh,string datefrom,string dateto)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            string[] date1 = datefrom.Split(' ')[0].Split('-');
            string[] date2 = dateto.Split(' ')[0].Split('-');
            DateTime dt1 = new DateTime(int.Parse(date1[0]), int.Parse(date1[1]), int.Parse(date1[2]));
            DateTime dt2 = new DateTime(int.Parse(date2[0]), int.Parse(date2[1]), int.Parse(date2[2]));
            return db.HoaDon.Where(x => x.MaKhachHang == makh && x.NgayLap >= dt1 && x.NgayLap <= dt2).ToList();
        }
        [HttpGet]
        public List<string> GetDS()
        {
            List<string> ds = new List<string>();
            RapPhimDBContext db = new RapPhimDBContext();
            int ve1ngay=0;
            int tongve=0;
            long doanhthungay=0;
            long doanhthuthang = 0;
            try
            {
                ve1ngay = (int)db.HoaDon.Where(x => x.NgayLap.Year == DateTime.Now.Year && x.NgayLap.Month == DateTime.Now.Month && x.NgayLap.Day == DateTime.Now.Day).Sum(x => x.SoVe);
            }
            catch (Exception)
            {
            }
            try
            {
                tongve = (int)db.HoaDon.Where(x => x.NgayLap.Year == DateTime.Now.Year && x.NgayLap.Month == DateTime.Now.Month).Sum(x => x.SoVe);
            }
            catch (Exception)
            {

            }
            try
            {
                doanhthungay = (long)db.HoaDon.Where(x => x.NgayLap.Year == DateTime.Now.Year && x.NgayLap.Month == DateTime.Now.Month && x.NgayLap.Day == DateTime.Now.Day).Sum(x => x.TongTien);
            }
            catch (Exception)
            {

            }
            try
            {
                doanhthuthang = (long)db.HoaDon.Where(x => x.NgayLap.Year == DateTime.Now.Year && x.NgayLap.Month == DateTime.Now.Month).Sum(x => x.TongTien);
            }
            catch (Exception)
            {

            }
            ds.Add(ve1ngay.ToString());
            ds.Add(tongve.ToString());
            ds.Add(doanhthungay.ToString());
            ds.Add(doanhthuthang.ToString());
            return ds;
        }
        [HttpGet]
        [Route("getdsrap")]
        public List<List<String>> GetDSRap()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            List<List<String>> tk = new List<List<String>>();
            List<String> rap =  db.Rap.Select(x=>x.TenRap).ToList();
            for(int i=0;i<rap.Count;i++)
            {
                tk.Add(new List<string>());
                tk[i].Add(rap[i]);
                tk[i].Add("0");
                try
                {
                    string r = rap[i].Trim();
                    string a = db.HoaDon.Where(x => x.Rap == r).Select(x => x.TongTien).Sum().ToString();
                    if (a != "")
                        tk[i][1] = a;
                }
                catch (Exception)
                {
                    tk[i].Add("0");
                }
            }
            return tk;
        }
        [HttpGet]
        [Route("getdsthang")]
        public List<List<String>> GetDSThang()
        {
            RapPhimDBContext db = new RapPhimDBContext();
            List<List<String>> tk = new List<List<String>>();
            List<DateTime> thang = new List<DateTime>();
            for (int i= 11;i>=0; i--){
                thang.Add(DateTime.Now.AddMonths(-i));
            }
            for (int i = 0; i < thang.Count; i++)
            {
                tk.Add(new List<string>());
                tk[i].Add(thang[i].ToShortDateString().Split('/')[1]+"/"+ thang[i].ToShortDateString().Split('/')[2]);
                tk[i].Add("0"); tk[i].Add("0");
                try
                {
                    int m = thang[i].Month;
                    int y = thang[i].Year;
                    string v = (db.HoaDon.Where(x => x.NgayLap.Year == y && x.NgayLap.Month == m).Select(x => x.SoVe).Sum()*100000).ToString();
                    if(v!="")
                        tk[i][1] = v;
                    string a = db.HoaDon.Where(x => x.NgayLap.Year == y && x.NgayLap.Month == m).Select(x => x.TongTien).Sum().ToString();
                    if (a != "")
                        tk[i][2] = a;
                }
                catch (Exception)
                {
                    tk[i].Add("0");
                }
            }
            return tk;
        }
        [HttpGet]
        [Route("getdskhach")]
        public List<ChitieuKH> GetDSKhach()
        {
            RapPhimDBContext db = new RapPhimDBContext();  
            return db.ChitieuKH.OrderByDescending(x=>x.Chitieu).Take(5).ToList();
        }
    }
}
