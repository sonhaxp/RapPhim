using System.Web;
using System.Web.Optimization;

namespace BanVeXemPhim
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery3.6").Include(
                        "~/Scripts/jquery-3.6.0.min.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/mycss").Include(
                      "~/Content/css/reset.css",
                      "~/Content/css/information.css",
                      "~/Content/css/style_BuyTicket.css",
                      "~/Content/css/style_chonve.css",
                      "~/Content/css/style_ghe.css",
                      "~/Content/css/style_thanhtoan.css",
                      "~/Content/css/base.css",
                      "~/Content/css/grid.css",
                      "~/Content/css/style_member.css",
                      "~/Content/css/style.css"));
            bundles.Add(new ScriptBundle("~/bundles/myjs_slider").Include(
                      "~/Scripts/js/main_slider.js"));
            bundles.Add(new ScriptBundle("~/bundles/myjs_member").Include(
                      "~/Scripts/js/main_member.js"));
            bundles.Add(new ScriptBundle("~/bundles/myjs_home").Include(
                      "~/Scripts/js/main_home.js"));
            bundles.Add(new ScriptBundle("~/bundles/myjs_choose-ticket").Include(
                      "~/Scripts/js/choose-ticket.js"));
            bundles.Add(new ScriptBundle("~/bundles/myjs_buy-ticket").Include(
                      "~/Scripts/js/buy-ticket.js"));
        }
    }
}
