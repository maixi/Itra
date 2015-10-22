using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.App_LocalResources;
namespace WebApplication7.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = GlobalRes.appPage;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = GlobalRes.contactpage;

            return View();
        }
    }

}