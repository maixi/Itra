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
        //poisk po demwotivatoram i ih vivdod
        [AllowAnonymous]
        public ActionResult TagResultDemosList(int id)
        {
            var tags = db.tag_to_dem.Where(d => d.tagId == id).ToList();
            List<Demotivator> demotivators = new List<Demotivator>();
            foreach (var item in tags)
            {
                var demotivator1 = db.Demotivators.Where(s => s.Id == item.DemotivatorId).ToList();
                foreach (var item1 in demotivator1)
                {
                    demotivators.Add(item1);
                }

            }
            return View(demotivators);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DemotivatorName,TopLine,BottomLine,OriginalImageUrl,DemotivatorUrl")] Demotivator demotivator, string Tags)
        {
            if (ModelState.IsValid)
            {
                demotivator.CreatorName = User.Identity.Name;
                demotivator.AspNetUserId = User.Identity.GetUserId();
                demotivator.Date = DateTime.Now;
                demotivator.Rate = "0,0,0,0,0";
                Demotivator dem = db.Demotivators.Add(demotivator);
                AddTags(Tags, dem);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DemotivatorName,Rate,CreatorName,DemotivatorUrl,OriginalImageUrl,TopLine,BottomLine,AspNetUserId")] Demotivator demotivator, string Tags)
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
        public void  DeleteRate(int id)
        {
            var rates = db.DemotivatorRates.Where(r => r.DemotivatorId == id).ToList();
            foreach (var item in rates)
            {
                db.DemotivatorRates.Remove(item);
            }
        }
        public void DeleteComments(int id)
        {
            var comments = db.Comments.Where(c => c.DemotivatorId == id).ToList();
            foreach (var item in comments)
            {
                db.Comments.Remove(item);
            }
        }
        public void DeleteTagsToDem(int id)
        {
            var tags_to_dem = db.tag_to_dem.Where(t => t.DemotivatorId == id).ToList();
            foreach (var item in tags_to_dem)
            {
                db.tag_to_dem.Remove(item);
                DeleteTag(item.tagId);
            }
        }
        public void DeleteTag(int id)
        {
            var tag = db.tags.FirstOrDefault(t => t.Id == id);
            if (tag.tag_to_dem.Count==0) db.tags.Remove(tag);
        }
        public void DeleteLikes(int id)
        {
            var likes = db.Likes.Where(h => h.CommentId == id).ToList();
            foreach (var item in likes)
            {
                db.Likes.Remove(item);
            }
        }
        public void DeleteALL (int id)
        {
            DeleteComments(id);
            DeleteRate(id);
            DeleteLikes(id);
            DeleteTagsToDem(id);
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
            DeleteALL(id);
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
        public void AddTags(string tagsline, Demotivator demotivator)
        {
            var tags = tagsline.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Distinct();
            
            foreach (var tagName in tags)
            {
                var TagName = tagName.TrimEnd(' ');
                if (!db.tags.Any(t => t.Name == TagName))
                {
                    tag tag = db.tags.Add(new tag() { Name = TagName});
                    var temp = db.tag_to_dem.Add(new tag_to_dem() { Demotivator = demotivator, tag = tag });
                }
                else
                {
                    var temp = db.tags.First(x => x.Name == TagName);
                        temp.tag_to_dem.Add(new tag_to_dem { Demotivator = demotivator, tag = temp });
                        db.Entry(temp).State = EntityState.Modified;
                }
            }

        }
        
        public ActionResult AutocompleteSearch(string term)
        {
            var models = db.tags.Where(a => a.Name.Contains(term))
                .Select(a => new { value = a.Name })
                .Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Upload()
        {
            bool flag = false;
            List<ImageUploadResult> UploadResultlist = new List<ImageUploadResult>();
            string fileName = "";
            Account account = new Account(Resources.GlobalResources.cloudName, Resources.GlobalResources.cloudApi, Resources.GlobalResources.cloudApiSecret);
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
