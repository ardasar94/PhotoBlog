using PhotoBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoBlog.Controllers
{
    public class HomeController : Controller
    {
        BlogDbContext db = new BlogDbContext();
        public ActionResult Index()
        {
            return View(db.BlogPosts.OrderByDescending(x => x.CreatingTime).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string tags)
        {
            List<BlogPost> FilteredPosts = new List<BlogPost>();
            if (tags.Trim() != null)
            {
                foreach (BlogPost post in db.BlogPosts)
                {
                    //List<HashTag> postTags = post.Tags.ToList(); ;
                    //foreach (var tag in postTags)
                    //{
                    //    if (tag.Name.ToLower() == tags.Trim().ToLower())
                    //    {
                    //        FilteredPosts.Add(post);
                    //        break;
                    //    }
                    //}
                    if (post.Title.ToLower().Contains(tags.Trim().ToLower()) || post.Content.ToLower().Contains(tags.Trim().ToLower()))
                    {
                        FilteredPosts.Add(post);
                    }
                }
            }
            if (FilteredPosts.Count > 0)
            {
                return View(FilteredPosts.OrderByDescending(x => x.CreatingTime).ToList());
            }
            else if (FilteredPosts.Count == 0)
            {
                TempData["NoData"] = "There are no post contains your key words";
            }
            return View(db.BlogPosts.OrderByDescending(x => x.CreatingTime).ToList());
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