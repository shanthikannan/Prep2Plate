 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Prep2Plate.Models;
 using RestSharp;

namespace Prep2Plate.Controllers
{
    public class WebApiController : Controller
    {
        private string url = "";
        private static HttpClient yumClient = new HttpClient();

        public WebApiController(string url)
        {
            var client = new RestClient("http://api.yummly.com/v1/api/recipes?_app_id=bf728886&_app_key=a1608c3e5cc021f4354aa6755f42c294&%7B%7Bsearch%7D%7D=&requirePictures=true&allowedDiet%5B%5D=390%5EPescetarian");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "c384ffee-8af8-ab3a-139e-f8310563af3f");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);

            return;
        }
        // GET: WebApi

        public ActionResult ShowRecipe(Recipe recipe)
        {   
            title = Console.WriteLine($"Name: {product.Name}\tPrice: {product.Price}\tCategory: {product.Category}");

            return View();
        }
    }
}