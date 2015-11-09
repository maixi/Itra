using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string term)
        {
            SearchModel model = new SearchModel();
            using (var elastic = new Elastic())
            {
                model.Demotivators = elastic.SearchByDemotivatorName(term);
                model.Users = elastic.SearchByUserName(term);
                model.term = term;
            }
            return View(model);
        }
        public JsonResult AutoCompleteSearch(string term)
        {
            List<Demotivator> demotivators = new List<Demotivator>();
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<string> response = new List<string>();
            using (var elastic = new Elastic())
            {
                demotivators = elastic.SearchByDemotivatorName(term);
                users = elastic.SearchByUserName(term);
                foreach (var dem in demotivators)
                {
                    response.Add(dem.DemotivatorName);
                }
                foreach (var user in users)
                {
                    response.Add(user.UserName);
                }
                //elastic.Delete(new Demotivator());
            }
            response = response.Distinct().ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}