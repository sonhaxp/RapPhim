using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BanVeXemPhim.Models.Entities;

namespace BanVeXemPhim.APIControllers
{
    public class VeBanController : ApiController
    {
        [HttpGet]
        public List<VeBan> GetSeatSelected(string masc)
        {
            RapPhimDBContext db = new RapPhimDBContext();
            return db.VeBan.Where(x => x.MaSuatChieu == masc).ToList();
        }
    }
}
