using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Week7LabPin.Migrations;
using Week7LabPin.Models;

namespace Week7LabPin.Controllers
{
    [Authorize]
    public class InterestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Interests
        public ActionResult Index()
        {
            return View(db.Interests.ToList());
        }

        public ActionResult GetImage(int Id)
        {
            var pin = db.Interests.Find(Id);
            return File(pin.Image, "image");
        }

        public ActionResult List()
        {
            var list = db.Interests.ToList().Select(p => new { p.URL, p.Link, p.Comment }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New(string ImageUrl, string Link, string Comment)
        {
            var currentUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var physicalByteImage = Configuration.GetImageByteArray(ImageUrl);
            var newinterest = new Interest() { URL = Link, Comment = Comment, Poster = currentUser, Image = physicalByteImage };
            db.Interests.Add(newinterest);
            db.SaveChanges();
            

            return Json(new { Link = newinterest.Link });
        }
    }
}
