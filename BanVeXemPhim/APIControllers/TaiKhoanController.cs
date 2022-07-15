using BanVeXemPhim.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace BanVeXemPhim.APIControllers
{
    public class TaiKhoanController : ApiController
    {
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
        [HttpGet]
        public TaiKhoan Login(string username)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            TaiKhoan a = db.TaiKhoan.Find(username);
            return a;
        }
        [HttpGet]
        public TaiKhoan Login(string username,string password)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            TaiKhoan a = db.TaiKhoan.Find(username);
            if (a == null) return null;
            if (a.MatKhau == GetMD5(password))
            {
                return a;
            }
            return null;
        }
        [HttpPost]
        public bool AddTK(string username,string password,string quyen)
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.TaiKhoan1 = username;
            taiKhoan.MatKhau = GetMD5(password);
            taiKhoan.Quyen = quyen;
            RapPhimDBContext db = new RapPhimDBContext();
            try
            {
                db.TaiKhoan.Add(taiKhoan);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        [HttpPut]
        public bool UpdateTaiKhoan(string username, string oldpassword, string newpassword)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            TaiKhoan tk = db.TaiKhoan.FirstOrDefault(x => x.TaiKhoan1 == username);
            if (tk.MatKhau == GetMD5(oldpassword))
                tk.MatKhau = GetMD5(newpassword);
            else return false;
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
        public bool DeleteTaiKhoan(string username)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            TaiKhoan tk = db.TaiKhoan.FirstOrDefault(x => x.TaiKhoan1 == username);
            try
            {
                db.TaiKhoan.Remove(tk);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
    }
}
