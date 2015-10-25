using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Demotivator> model = new List<Demotivator>();
                   using (var context = new Entities())
                   {
                    model = context.Demotivators.ToList();
                   }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}