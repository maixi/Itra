using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ApplicationUser> model = new List<ApplicationUser>();
              if (User.Identity.IsAuthenticated)
               {
                   using (var context = new ApplicationDbContext())
                   {
                        model = context.Users.Where(x => x.EmailConfirmed == true).ToList();
                   }
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
        
        [Authorize]
        public ActionResult DeleteUser(string userId)
        {
                   
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Find(userId);
                             
                context.Users.Remove(user);
                context.SaveChanges();
                if (user.Email == User.Identity.Name)
                    HttpContext.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);

            }
            return RedirectToAction("Index");
        }
    }
}