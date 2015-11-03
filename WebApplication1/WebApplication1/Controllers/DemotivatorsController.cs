using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication1.Filters;
using WebApplication1.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;
namespace WebApplication1.Controllers
{
    [Culture]
    public class DemotivatorsController : Controller
    {
        private Entities db = new Entities();

        // GET: Demotivators
        public ActionResult Index()
        {

            var demotivators = db.Demotivators.Include(d => d.AspNetUser);
            return View(demotivators.ToList());
        }

        // GET: Demotivators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demotivator demotivator = db.Demotivators.Find(id);
            if (demotivator == null)
            {
                return HttpNotFound();
            }
            return View(demotivator);
        }

        // GET: Demotivators/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Demotivators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DemotivatorName,Rate,CreatorName,DemotivatorUrl,OriginalImageUrl,TopLine,BottomLine,AspNetUserId")] Demotivator demotivator)
        {
            if (ModelState.IsValid)
            {
                demotivator.BottomLine = "";
                demotivator.TopLine = "";
                demotivator.Rate = "";
                demotivator.DemotivatorName = "";
                demotivator.OriginalImageUrl = "";
                demotivator.DemotivatorUrl = "";
                demotivator.CreatorName = User.Identity.Name;
                demotivator.AspNetUserId = User.Identity.GetUserId();
                demotivator.Date = DateTime.Now;
                db.Demotivators.Add(demotivator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", demotivator.AspNetUserId);
            return View(demotivator);
        }

        // GET: Demotivators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demotivator demotivator = db.Demotivators.Find(id);
            if (demotivator == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", demotivator.AspNetUserId);
            return View(demotivator);
        }

        // POST: Demotivators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DemotivatorName,Rate,CreatorName,DemotivatorUrl,OriginalImageUrl,TopLine,BottomLine,AspNetUserId")] Demotivator demotivator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demotivator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", demotivator.AspNetUserId);
            return View(demotivator);
        }

        // GET: Demotivators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demotivator demotivator = db.Demotivators.Find(id);
            if (demotivator == null)
            {
                return HttpNotFound();
            }
            return View(demotivator);
        }

        // POST: Demotivators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demotivator demotivator = db.Demotivators.Find(id);
            db.Demotivators.Remove(demotivator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult AddComment(string TextMessange, int IdDem)
        {
            var comment = new Comment();
            comment.PublicationDate = DateTime.Now;
            comment.AspNetUserId = User.Identity.GetUserId();        
            comment.CommentText = TextMessange;
            comment.DemotivatorId = IdDem;
            db.Comments.Add(comment);
            db.SaveChanges();
            var returnFckingJson = new
            {
                UserName = User.Identity.Name,
                Text = comment.CommentText
            };
            return Json(returnFckingJson, JsonRequestBehavior.AllowGet);
        }
    }
}
