using System;
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
    public class UserSetController : Controller
    {
        // GET: UsetSet
        private Entities db = new Entities();
        public ActionResult Index()
        {
            UserSetViewModel HomeParamsToLoad = new UserSetViewModel();
            var tags = db.tags.ToList();
            var newDemotivators = db.Demotivators.OrderByDescending(x => x.Date).Take(50).Include(s => s.Comments).
                Include(s => s.AspNetUser).
                Include(s => s.DemotivatorRates).
                Include(s => s.tag_to_dem).ToList();          
            
            HomeParamsToLoad.DemCount = db.Demotivators.Count();
            var Demotivators = db.Demotivators.Include(s => s.Comments).
                Include(s => s.AspNetUser).
                Include(s => s.DemotivatorRates).
                Include(s => s.tag_to_dem).ToList();         
            HomeParamsToLoad.Demotivators = Demotivators;
            HomeParamsToLoad.DemCount = db.Demotivators.Count();
            return View(HomeParamsToLoad);
        }
    }
}