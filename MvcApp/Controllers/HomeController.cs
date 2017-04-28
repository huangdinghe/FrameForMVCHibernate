using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : UcController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "重庆惠科金渝光电班车订座系统";
            return View();
        }
        public ActionResult IndexMobile()
        {
            ViewBag.Title = "重庆惠科金渝光电班车订座系统";
            return View();
        }
        public ActionResult IndexDriverMobile()
        {
            ViewBag.Title = "重庆惠科金渝光电班车订座系统";
            return View();
        }
    }
}
