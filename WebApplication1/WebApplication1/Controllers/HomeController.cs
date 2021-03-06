﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using WebApplication1;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication1.Models;
using WebApplication1.Filters;


namespace WebApplication1.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            HomeViewModel HomeParamsToLoad = new HomeViewModel();
            var tags = db.tags.ToList();
            var demotivators = db.Demotivators.Include(s => s.Comments).
                 Include(s => s.AspNetUser).
                Include(s => s.DemotivatorRates).
                Include(s => s.tag_to_dem).ToList();
            var newDemotivators = demotivators.OrderByDescending(x => x.Date).Take(50).ToList();
            var bestDemotivators = demotivators.OrderByDescending(x => getRating(x.Rate)).Take(50).ToList();
            var users = new List <ApplicationUser>();            
            HomeParamsToLoad.tags = tags;
            HomeParamsToLoad.DemCount = db.Demotivators.Count();
            HomeParamsToLoad.newDemotivators = newDemotivators;
            HomeParamsToLoad.bestDemotivators = bestDemotivators;
            HomeParamsToLoad.TopUsers = GetTopUserByRating();        
            return View(HomeParamsToLoad);
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
        public double getRating(string rateStr)
        {
            var rates = rateStr.Split(',').ToList();
            double rating = 0;
            int divider = 0;
            for (int i = 0; i < rates.Count; i++)
            {
                rating += Int32.Parse(rates[i]) * (i + 1);
                divider += Int32.Parse(rates[i]);
            }
            if (divider != 0) rating /= divider;
            else rating = 0;
            return rating;
        }
        public double GetAverageUserRating(List<Demotivator> demotivators)
        {
            double averageRate = 0;
            foreach (var dem in demotivators)
                averageRate += getRating(dem.Rate);
            if (demotivators.Count != 0)  averageRate /= demotivators.Count;
            else averageRate = 0;
            return averageRate;
        }
        public List<AspNetUser> GetTopUserByRating()
        {
            var users = db.AspNetUsers.ToList();
            var top = users.OrderByDescending(u => GetAverageUserRating(db.Demotivators.Where(d => d.AspNetUserId == u.Id).ToList())).Take(5).ToList();
            return top;
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