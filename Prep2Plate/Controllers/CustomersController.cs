using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customer = new Customers() {FirstName = "Hello! Welcome to Prep2Plate."};
            return View(customer);
        }

        public ActionResult New()
        {
            //var DietPreference = _context.DietPreferences.ToList();
            return View();
        }
        
    }
}