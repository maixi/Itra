using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebApplication1.Filters;
using System.Data.Entity;
using System.Data;
using Microsoft.AspNet.Identity;


namespace WebApplication1.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private Entities db = new Entities();
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

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
        public JsonResult PreRating(string r, string s, string id, string url)
        {
            int autoId = 0;
            int thisVote = 0;
            int.TryParse(r, out thisVote);
            int.TryParse(id, out autoId);
            var Cir = User.Identity.GetUserId();
            var isIt = db.DemotivatorRates.Where(v => v.AspNetUserId == Cir && v.DemotivatorId == autoId).FirstOrDefault();
            if (isIt != null)
            {
                // keep the school voting flag to stop voting by this member
                HttpCookie cookie = new HttpCookie(url, "true");
                Response.Cookies.Add(cookie);
                return Json("<br />You have already rated this post, thanks !");
            }
            return Json("");
        }
        public JsonResult SendRating(string r, string s, string id, string url)
        {
            int autoId = 0;
            int thisVote = 0;
            int.TryParse(r, out thisVote);
            int.TryParse(id, out autoId);

            if (!User.Identity.IsAuthenticated)
            {
                return Json("Not authenticated!");
            }

            if (autoId.Equals(0))
            {
                return Json("Sorry, record to vote doesn't exists");
            }

            // check if he has already voted
            var Cir = User.Identity.GetUserId();
            var isIt = db.DemotivatorRates.Where(v => v.AspNetUserId == Cir && v.DemotivatorId == autoId).FirstOrDefault();
            if (isIt != null)
            {
                // keep the school voting flag to stop voting by this member
                HttpCookie cookie = new HttpCookie(url, "true");
                Response.Cookies.Add(cookie);
                return Json("<br />You have already rated this post, thanks !");
            }

            var PeremennyaDlyaObjectRate = db.Demotivators.Where(sc => sc.Id == autoId).FirstOrDefault();
            if (PeremennyaDlyaObjectRate != null)
            {
                object obj = PeremennyaDlyaObjectRate.Rate;

                string updatedVotes = string.Empty;
                string[] votes = null;
                if (obj != null && obj.ToString().Length > 0)
                {
                    string currentVotes = obj.ToString(); // votes pattern will be 0,0,0,0,0
                    votes = currentVotes.Split(',');
                    // if proper vote data is there in the database
                    if (votes.Length.Equals(5))
                    {
                        // get the current number of vote count of the selected vote, always say -1 than the current vote in the array 
                        int currentNumberOfVote = int.Parse(votes[thisVote - 1]);
                        // increase 1 for this vote
                        currentNumberOfVote++;
                        // set the updated value into the selected votes
                        votes[thisVote - 1] = currentNumberOfVote.ToString();
                    }
                    else
                    {
                        votes = new string[] { "0", "0", "0", "0", "0" };
                        votes[thisVote - 1] = "1";
                    }
                }
                else
                {
                    votes = new string[] { "0", "0", "0", "0", "0" };
                    votes[thisVote - 1] = "1";
                }

                // concatenate all arrays now
                foreach (string ss in votes)
                {
                    updatedVotes += ss + ",";
                }
                updatedVotes = updatedVotes.Substring(0, updatedVotes.Length - 1);

                db.Entry(PeremennyaDlyaObjectRate).State = EntityState.Modified;
                PeremennyaDlyaObjectRate.Rate = updatedVotes;
                db.SaveChanges();

                DemotivatorRate vm = new DemotivatorRate()
                {
                    AspNetUserId = User.Identity.GetUserId(),
                    Mark = thisVote.ToString(),
                    DemotivatorId = autoId,
                };

                db.DemotivatorRates.Add(vm);

                db.SaveChanges();

                // keep the school voting flag to stop voting by this member
                HttpCookie cookie = new HttpCookie(url, "true");
                Response.Cookies.Add(cookie);
            }
            return Json("<br />You rated " + r + " star(s), thanks !");
        }
    }
}