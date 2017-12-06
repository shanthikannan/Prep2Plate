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
            return View(db.UserPreference.ToList());
        }

        // GET: UserPreferences/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreference userPreference = db.UserPreference.Find(id);
            if (userPreference == null)
            {
                return HttpNotFound();
            }
            return View(userPreference);
        }

        // GET: UserPreferences/Create
        public ActionResult Create()
        {
            UserPreference db = new UserPreference();


            return View();
        }

        // POST: UserPreferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Preferences")] UserPreference userPreference)
        {
            if (ModelState.IsValid)
            {
                db.UserPreference.Add(userPreference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userPreference);
        }

        // GET: UserPreferences/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreference userPreference = db.UserPreference.Find(id);
            if (userPreference == null)
            {
                return HttpNotFound();
            }
            return View(userPreference);
        }

        // POST: UserPreferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Preferences")] UserPreference userPreference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPreference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userPreference);
        }

        // GET: UserPreferences/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreference userPreference = db.UserPreference.Find(id);
            if (userPreference == null)
            {
                return HttpNotFound();
            }
            return View(userPreference);
        }

        // POST: UserPreferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserPreference userPreference = db.UserPreference.Find(id);
            db.UserPreference.Remove(userPreference);
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
