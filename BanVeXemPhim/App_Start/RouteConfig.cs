using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanVeXemPhim
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Trang chủ",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "Thông tin",
                url: "thong-tin/{id}",
                defaults: new { controller = "InfoPhim", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Thành viên",
                url: "thanh-vien",
                defaults: new { controller = "Member", action = "Index" }
            );
            routes.MapRoute(
                name: "Đặt vé",
                url: "chon-ve/{id}",
                defaults: new { controller = "Choose_Ticket", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Tìm kiếm bởi tên",
                url: "tim-kiem/{id}",
                defaults: new { controller = "Search", action = "SearchByKey", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Mua vé",
                url: "mua-ve",
                defaults: new { controller = "BuyTicket", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
