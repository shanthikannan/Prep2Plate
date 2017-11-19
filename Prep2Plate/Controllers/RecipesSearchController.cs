using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Context;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class RecipesSearchController : Controller
    {
        private RecipeContext db = new RecipeContext();

        // GET: RecipeSearchResults
        public ActionResult Index()
        {
            return View(db.RecipeSearchResults.ToList());
        }

        // GET: RecipeSearchResults/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
            if (recipeSearchResult == null)
            {
                return HttpNotFound();
            }
            return View(recipeSearchResult);
        }

        // GET: RecipeSearchResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeSearchResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImageUrl,RecipeName")] RecipeSearchResult recipeSearchResult)
        {
            if (ModelState.IsValid)
            {
                db.RecipeSearchResults.Add(recipeSearchResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipeSearchResult);
        }

        // GET: RecipeSearchResults/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
            if (recipeSearchResult == null)
            {
                return HttpNotFound();
            }
            return View(recipeSearchResult);
        }

        // POST: RecipeSearchResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageUrl,RecipeName")] RecipeSearchResult recipeSearchResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeSearchResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipeSearchResult);
        }

        // GET: RecipeSearchResults/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
            if (recipeSearchResult == null)
            {
                return HttpNotFound();
            }
            return View(recipeSearchResult);
        }

        // POST: RecipeSearchResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
            db.RecipeSearchResults.Remove(recipeSearchResult);
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
