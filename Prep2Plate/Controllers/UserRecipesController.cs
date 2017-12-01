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
            UserRecipe userRecipe = db.UserRecipes.Find(id);
            if (userRecipe == null)
            {
                return HttpNotFound();
            }
            return View(userRecipe);
        }

        // GET: UserRecipes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRecipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,UserName,ImageUrl,RecipeName,Ingredients,RecipeSourceUrl")] UserRecipe userRecipe)
        {
            if (ModelState.IsValid)
            {
                db.UserRecipes.Add(userRecipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRecipe);
        }

        // GET: UserRecipes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRecipe userRecipe = db.UserRecipes.Find(id);
            if (userRecipe == null)
            {
                return HttpNotFound();
            }
            return View(userRecipe);
        }

        // POST: UserRecipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,UserName,ImageUrl,RecipeName,Ingredients,RecipeSourceUrl")] UserRecipe userRecipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRecipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            UserRecipe userRecipe = db.UserRecipes.Find(id);
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
            UserRecipe userRecipe = db.UserRecipes.Find(id);
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
