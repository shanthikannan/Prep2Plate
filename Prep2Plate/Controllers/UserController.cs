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
        // GET: User/UserDetails
        public ActionResult UserDetails()
        {
            var user = new User();
            return View(user);
        }
    }
}