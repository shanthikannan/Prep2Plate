using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            return View()
        }

        public ViewResult Index()
        {
            //calling users from db
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: User/UserDetails
        public ActionResult UserDetails(int id)
        {
            var user = _context.Users.SingleOrDefault(c => c.Id == id);
            if (user == null)
                return HttpNotFound();
            return View(user);


        }
    }
}
