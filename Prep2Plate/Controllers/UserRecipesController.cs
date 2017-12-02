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
    public class UserRecipesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRecipes
        public ActionResult Index()
        {
            return View(db.UserRecipes.Where(userRecipe => userRecipe.UserName.Equals(User.Identity.Name)).ToList());
        }

        // GET: UserRecipes/Details/5
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
      

        // GET: UserRecipes/Delete/5
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

        // POST: UserRecipes/Delete/5
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
