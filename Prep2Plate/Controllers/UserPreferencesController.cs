using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class UserPreferencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserPreferences
        public ActionResult Index()
        {
            return View(db.UserPreferences.ToList());
        }

        [HttpPost]
        public ActionResult Prep()
        {
            //Store the user selected preference to the database
            return RedirectToAction("Index", "RecipesSearch");
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
