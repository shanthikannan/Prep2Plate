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
    [Authorize]
    public class UserRecipesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.UserRecipes.Where(userRecipe => userRecipe.UserName.Equals(User.Identity.Name)).ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRecipe userRecipe = db.UserRecipes.Find(id, User.Identity.Name);
            if (userRecipe == null)
            { 
                return HttpNotFound();
            }

            return View(userRecipe);
        }

        public ActionResult AddToCalendar(string id)
        {
            return RedirectToAction("Index", "RecipeCalendar", new { id = id });
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRecipe userRecipe = db.UserRecipes.Find(id, User.Identity.Name);
            if (userRecipe == null)
            {
                return HttpNotFound();
            }
           
            return View(userRecipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserRecipe userRecipe = db.UserRecipes.Find(id, User.Identity.Name);
            db.UserRecipes.Remove(userRecipe);
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
