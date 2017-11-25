 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class WebApiController : Controller
    {
        private string url = "";
        private static HttpClient yumClient = new HttpClient();

        public YumHelper(string url)
        {
            this.url = url;

            return  
        }
        // GET: WebApi

        public ActionResult ShowRecipe(Recipe recipe)
        {   
            title = Console.WriteLine($"Name: {product.Name}\tPrice: {product.Price}\tCategory: {product.Category}");

            return View();
        }
    }
}