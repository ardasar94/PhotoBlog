using PhotoBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoBlog.Areas.Admin.Controllers
{
    public abstract class AdminBaseController : Controller
    {
        protected BlogDbContext db = new BlogDbContext();
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