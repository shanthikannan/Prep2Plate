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
    [Authorize]
    public class RecipeCalendarController : Controller
    {
        private static string _recipeName;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string id)
        {
            if (id == null)
            {
                ViewData["clickable"] = false;
            }
            else
            {
                ViewData["clickable"] = true;
            }
            _recipeName = id;
            return View(db.RecipeCalendar.ToList());
        }


        public ActionResult SaveRecipeToCalendar(int dayOfWeek, int type)
        {
            RecipeCalendarData calendarData = new RecipeCalendarData();
            calendarData.UserName = User.Identity.Name;
            calendarData.DayOfTheWeek = dayOfWeek;
            calendarData.TypeOfMeal = type;
            calendarData.RecipeName = _recipeName;
            db.RecipeCalendar.AddOrUpdate(calendarData);
            db.SaveChanges();
            return RedirectToAction("Index", new {id = _recipeName});
        }

        public ActionResult RemoveRecipeFromCalendar(int dayOfWeek, int type)
        {
            RecipeCalendarData calendarData =  db.RecipeCalendar.Find(User.Identity.Name, dayOfWeek, type);
            db.RecipeCalendar.Remove(calendarData);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = _recipeName });
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