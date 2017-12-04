using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Context;

namespace Prep2Plate.Controllers
{
    public class PreferencesController : Controller
    {
        
        // GET: Preferences
        public ActionResult Index()
        {
            return View();
        }

        // GET: Preferences/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Preferences/Create
        public ActionResult SavePreferences(Models.Customers model)
        {
            RecipeContext db = new RecipeContext();
           
            
            Models.Customers cust = new Models.Customers();
            cust.Id = model.Id;
            cust.Diet = model.Diet;
            cust.PreferredCookingDay = model.PreferredCookingDay;
            cust.PreferredShoppingDay = model.PreferredShoppingDay;

           
            
            return View();
        }

        // POST: Preferences/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("RecipesSearch");
            }
            catch
            {
                return View();
            }
        }

        // GET: Preferences/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Preferences/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Preferences/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Preferences/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
