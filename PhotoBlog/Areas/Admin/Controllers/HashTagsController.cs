using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoBlog.Models;

namespace PhotoBlog.Areas.Admin.Controllers
{
    public class HashTagsController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Admin/HashTags
        public ActionResult Index()
        {
            return View(db.HashTags.ToList());
        }

        // GET: Admin/HashTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HashTag hashTag = db.HashTags.Find(id);
            if (hashTag == null)
            {
                return HttpNotFound();
            }
            return View(hashTag);
        }

        // GET: Admin/HashTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HashTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] HashTag hashTag)
        {
            if (ModelState.IsValid)
            {
                db.HashTags.Add(hashTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hashTag);
        }

        // GET: Admin/HashTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HashTag hashTag = db.HashTags.Find(id);
            if (hashTag == null)
            {
                return HttpNotFound();
            }
            return View(hashTag);
        }

        // POST: Admin/HashTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] HashTag hashTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hashTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hashTag);
        }

        // GET: Admin/HashTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HashTag hashTag = db.HashTags.Find(id);
            if (hashTag == null)
            {
                return HttpNotFound();
            }
            return View(hashTag);
        }

        // POST: Admin/HashTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HashTag hashTag = db.HashTags.Find(id);
            db.HashTags.Remove(hashTag);
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
    }
}
