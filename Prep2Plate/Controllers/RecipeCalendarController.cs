using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class RecipeCalendarController : Controller
    {
        private static string recipeId;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecipeCalendar
        public ActionResult Index(string id)
        {
            recipeId = id;
            return View(db.RecipeCalendar.ToList());
        }

        // GET: RecipeCalendar/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeCalendarData recipeCalendarData = db.RecipeCalendar.Find(id);
            if (recipeCalendarData == null)
            {
                return HttpNotFound();
            }
            return View(recipeCalendarData);
        }


        // GET: RecipeCalendar/Details/5
        public ActionResult SaveRecipeToCalendar(int dayOfWeek, int type)
        {
            RecipeCalendarData calendarData = new RecipeCalendarData();
            calendarData.UserName = User.Identity.Name;
            calendarData.DayOfTheWeek = dayOfWeek;
            calendarData.TypeOfMeal = type;
            calendarData.RecipeId = recipeId;
            db.RecipeCalendar.AddOrUpdate(calendarData);
            db.SaveChanges();
            return RedirectToAction("Index", new {id = recipeId});
        }


        // GET: RecipeCalendar/Details/5
        public ActionResult RemoveRecipeFromCalendar(int dayOfWeek, int type)
        {
            RecipeCalendarData calendarData = new RecipeCalendarData();
            calendarData.UserName = User.Identity.Name;
            calendarData.DayOfTheWeek = dayOfWeek;
            calendarData.TypeOfMeal = type;
            db.RecipeCalendar.Remove(calendarData);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = recipeId });
        }

        // GET: RecipeCalendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeCalendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,DayOfTheWeek,TypeOfMeal,RecipeId")] RecipeCalendarData recipeCalendarData)
        {
            if (ModelState.IsValid)
            {
                db.RecipeCalendar.Add(recipeCalendarData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipeCalendarData);
        }

        // GET: RecipeCalendar/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeCalendarData recipeCalendarData = db.RecipeCalendar.Find(id);
            if (recipeCalendarData == null)
            {
                return HttpNotFound();
            }
            return View(recipeCalendarData);
        }

        // POST: RecipeCalendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,DayOfTheWeek,TypeOfMeal,RecipeId")] RecipeCalendarData recipeCalendarData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeCalendarData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipeCalendarData);
        }

        // GET: RecipeCalendar/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipeCalendarData recipeCalendarData = db.RecipeCalendar.Find(id);
            if (recipeCalendarData == null)
            {
                return HttpNotFound();
            }
            return View(recipeCalendarData);
        }

        // POST: RecipeCalendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RecipeCalendarData recipeCalendarData = db.RecipeCalendar.Find(id);
            db.RecipeCalendar.Remove(recipeCalendarData);
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
