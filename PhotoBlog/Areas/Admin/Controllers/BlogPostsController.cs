using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoBlog.Models;

namespace PhotoBlog.Areas.Admin.Controllers
{
    public class BlogPostsController : AdminBaseController
    {

        public ActionResult Index()
        {
            return View(db.BlogPosts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        public ActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(db.HashTags.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPost blogPost, HttpPostedFileBase photo, string[] tags)
        {
            ViewBag.Tags = new MultiSelectList(db.HashTags.ToList(), "Id", "Name");
            PhototErrorControls(photo);
            if (blogPost != null && photo!= null)
            {
                var allTags = tags[1];
                string[] tagFinal = allTags.Split(',');
                for (int i = 0; i < tagFinal.Length; i++)
                {
                    var oneTag = tagFinal[i];
                    if (!db.HashTags.Any(x=>x.Name == oneTag))
                    {
                        HashTag addedTag = new HashTag() { Name = tagFinal[i] };
                        db.HashTags.Add(addedTag);
                        blogPost.Tags.Add(addedTag);
                    }
                    else
                    {
                        HashTag tagAlreadyCreated = db.HashTags.FirstOrDefault(x => x.Name == oneTag);
                        blogPost.Tags.Add(tagAlreadyCreated);
                    }
                }
                blogPost.CreatingTime = DateTime.Now;
                blogPost.PhotoPath = PhotoUpdate(photo);
                db.BlogPosts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.tags = blogPost.Tags.ToList();
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PhotoPath,CreatingTime")] BlogPost blogPost, HttpPostedFileBase photo, string[] tags)
        {
            PhototErrorControls(photo);
            if (blogPost != null && photo != null)
            {
                var allTags = tags[1];
                string[] tagFinal = allTags.Split(',');
                for (int i = 0; i < tagFinal.Length; i++)
                {
                    var oneTag = tagFinal[i];
                    if (!db.HashTags.Any(x => x.Name == oneTag))
                    {
                        HashTag addedTag = new HashTag() { Name = tagFinal[i] };
                        db.HashTags.Add(addedTag);
                        blogPost.Tags.Add(addedTag);
                    }
                    else
                    {
                        HashTag tagAlreadyCreated = db.HashTags.FirstOrDefault(x => x.Name == oneTag);
                        blogPost.Tags.Add(tagAlreadyCreated);
                    }
                }

                var photoPath = PhotoUpdate(photo);
                if (photoPath != null)
                {
                    DeletePhoto(blogPost.PhotoPath);
                    blogPost.PhotoPath = photoPath;
                }
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            DeletePhoto(blogPost.PhotoPath);
            return RedirectToAction("Index");
        }

        private void DeletePhoto(string photoPath)
        {
            if (!string.IsNullOrEmpty(photoPath))
            {
                var DelPath = Path.Combine(Server.MapPath("~/Images/Uploads"), photoPath);

                if (System.IO.File.Exists(DelPath))
                {
                    System.IO.File.Delete(DelPath);
                }
            }
        }

        private void PhototErrorControls(HttpPostedFileBase photo)
        {
            string[] alloweds = { ".jpg", ".jpeg", ".png" };
            if (photo != null)
            {
                if (!alloweds.Contains(Path.GetExtension(photo.FileName).ToLower()))
                {
                    ModelState.AddModelError("photo", "İzin verilen dosya uzantıları: .jpg, .jpeg, .png");
                }
                else if (photo.ContentLength > 10000 * 10000)
                {
                    ModelState.AddModelError("photo", "Resim boyutu 100mb'dan küçük olmalıdır");
                }

            }
        }

        private string PhotoUpdate(HttpPostedFileBase photo)
        {
            if (photo == null)
                return null;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            var installFilePath = Server.MapPath("~/Images/Uploads");
            var savePath = Path.Combine(installFilePath, fileName);
            photo.SaveAs(savePath);
            return fileName;
        }
    }
}
