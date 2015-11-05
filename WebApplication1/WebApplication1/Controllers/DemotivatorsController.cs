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
using System.IO;
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
        public ActionResult TopDemotivators()
        {
            List<Demotivator> model = new List<Demotivator>();
            using (var context = new Entities())
            {
                model = context.Demotivators.ToList();
            }
            return View(model);
        }
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
        public ActionResult Create([Bind(Include = "DemotivatorName,TopLine,BottomLine,OriginalImageUrl,DemotivatorUrl")] Demotivator demotivator)
        {
            if (ModelState.IsValid)
            {
                demotivator.CreatorName = User.Identity.Name;
                demotivator.AspNetUserId = User.Identity.GetUserId();
                demotivator.Date = DateTime.Now;
                demotivator.Rate ="0,0,0,0,0";
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
            var ret = new
            {

                UserName = User.Identity.Name,
                PublicationDate = comment.PublicationDate.ToString("s"),
                CommentText = comment.CommentText
            };
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Upload()
        {
            bool flag = false;
            List<ImageUploadResult> UploadResultlist = new List<ImageUploadResult>();
            string fileName = "";
            Account account = new Account("djb7hr8nk", "981823513498862", "ortnIAykexsGNcYTGiMTNjIarvo");
            CloudinaryDotNet.Cloudinary cloudinary = new Cloudinary(account);
            ImageUploadResult uploadResultImg = new ImageUploadResult();
            ImageUploadResult uploadResultDemotivator = new ImageUploadResult();
            ImageUploadParams uploadParamsImg = new ImageUploadParams();
            ImageUploadParams uploadParamsDemotivator = new ImageUploadParams();
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                    uploadParamsImg = new ImageUploadParams()
                    {
                        File = new FileDescription(Server.MapPath("~/Files/" + fileName)),
                        PublicId = User.Identity.Name + fileName + DateTime.Now.Millisecond.ToString(),
                    };
                }
            }
            foreach (string file in Request.Form)
            {
                var upload = Request.Form[file];
                if (upload != null)
                {
                    if (upload.Contains("data:image/png;base64"))
                    {
                        string x = upload.Replace("data:image/png;base64,", "");
                        byte[] imageBytes = Convert.FromBase64String(x);
                        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);


                        ms.Write(imageBytes, 0, imageBytes.Length);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                        image.Save(Server.MapPath("~/Files/img.png"), System.Drawing.Imaging.ImageFormat.Png);

                        uploadParamsDemotivator = new ImageUploadParams()
                        {
                            File = new FileDescription(Server.MapPath("~/Files/img.png")),
                            PublicId = User.Identity.Name + fileName + "demotivator" + DateTime.Now.Millisecond.ToString()
                        };
                    }
                    else
                    {
                        flag = true;
                        uploadParamsImg = new ImageUploadParams()
                        {
                            File = new FileDescription(upload),
                            PublicId = User.Identity.Name + fileName + DateTime.Now.Millisecond.ToString()
                        };
                    }
                }
            }

            uploadResultImg = cloudinary.Upload(uploadParamsImg);
            UploadResultlist.Add(uploadResultImg);
            uploadResultDemotivator = cloudinary.Upload(uploadParamsDemotivator);
            UploadResultlist.Add(uploadResultDemotivator);
            if (!flag) System.IO.File.Delete(Server.MapPath("~/Files/" + fileName));
            System.IO.File.Delete(Server.MapPath("~/Files/img.png"));
            return Json(UploadResultlist, JsonRequestBehavior.AllowGet);
        }
    }
}
