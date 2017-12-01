using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prep2Plate.Context;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    [Authorize]
    public class RecipesSearchController : Controller
    {
        private RecipeContext db = new RecipeContext();        
        ////Hosted web API REST Service base url  
        private string AppId = "bf728886";
        private string AppKey = "a1608c3e5cc021f4354aa6755f42c294";
        private string Baseurl = "http://api.yummly.com/v1/";

        // Function called before the Page is loaded
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
            } else { 
                ClearDatabase();
            }
            return View(db.RecipeSearchResults.ToList());
        }

        public ActionResult OnSearchRecipe(string searchRecipe)
        {
            ClearDatabase();
            string _requestUrl = "api/recipes?_app_id=" + AppId + "&_app_key=" + AppKey + "&requirePictures=true&q=" + searchRecipe;
            string httpResponse = CallYumlyApi(_requestUrl);
            bool result = ParseSearchResultsResponseAndStoreInDb(httpResponse);
            if (result)
            {
                return RedirectToAction("Index", new { id = -1 });
            }
            else
            {
                //Show Error Message
                return RedirectToAction("About", "Home");
            }
        }

        private string CallYumlyApi(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    var htttpResponse = client.GetAsync(requestUrl);
                    htttpResponse.Wait();
                    var htttpResult = htttpResponse.Result;

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (!htttpResult.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    return htttpResult.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

        private bool ParseSearchResultsResponseAndStoreInDb(string httpResponse)
        {
            if (httpResponse == null)
            {
                return false;
            }
            JObject jObect = JObject.Parse(httpResponse);
            IList<JToken> jTokenList = jObect["matches"].Children().ToList();
                
            // serialize JSON results into .NET objects
            foreach(JToken jToken in jTokenList)
            {
                RecipeSearchResult recipeSearchResult = new RecipeSearchResult
                {
                    Id = jToken["id"].ToString(),
                    RecipeName = jToken["recipeName"].ToString(),
                    ImageUrl = jToken["imageUrlsBySize"]["90"].ToString()
                };
                db.RecipeSearchResults.Add(recipeSearchResult);
            }
            db.SaveChanges();
            return true;
        }

        public bool ParseGetResultsResponseAndUpdateInDb(string httpResponse, RecipeSearchResult recipeSearchResult)
        {
            JObject jObect = JObject.Parse(httpResponse);
            recipeSearchResult.Ingredients = jObect["ingredientLines"].ToString();
            //Get the sourceRecipeUrl and store it in the model
            recipeSearchResult.RecipeSourceUrl = jObect["source"]["sourceRecipeUrl"].ToString();
            db.SaveChanges();
            return false;
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
            if (recipeSearchResult.Ingredients == null)
            {
                string _requestUrl = "api/recipe/" + recipeSearchResult.Id + "?_app_id=" + AppId + "&_app_key=" + AppKey;
                string httpResponse = CallYumlyApi(_requestUrl);
                bool result = ParseGetResultsResponseAndUpdateInDb(httpResponse, recipeSearchResult);
            }
            return View(recipeSearchResult);
        }

        private void ClearDatabase()
        {
            foreach (var recipeSearchResult in db.RecipeSearchResults)
            {
                db.RecipeSearchResults.Remove(recipeSearchResult);
            }
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult SaveRecipe()
        {
            Console.WriteLine(User.Identity.Name);
            return RedirectToAction("Index", new { id = -1 });
        }
    }
}
