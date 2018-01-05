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
using Newtonsoft.Json.Linq;
using Prep2Plate.Context;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    [Authorize]
    public class RecipesSearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();        
        ////Hosted web API REST Service base url  
        private string AppId = "bf728886";
        private string AppKey = "a1608c3e5cc021f4354aa6755f42c294";
        private string Baseurl = "http://api.yummly.com/v1/";

        // Function called before the Page is loaded
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
            }
            else
            {
                ClearDatabase();
            }
            return View(db.RecipeSearchResults.ToList());
        }

        public ActionResult OnSearchRecipe(string searchRecipe)
        {
            try
            {
                ClearDatabase();
                string _requestUrl = "api/recipes?_app_id=" + AppId + "&_app_key=" + AppKey +
                                     "&requirePictures=true&q=" + searchRecipe;
                string httpResponse = CallYumlyApi(_requestUrl);
                ParseSearchResultsResponseAndStoreInDb(httpResponse);
                return RedirectToAction("Index", new {id = -1});
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = GetErrorMessage(e);
                return RedirectToAction("Error");
            }
        }

        private string GetErrorMessage(Exception e)
        {
            string errorMessage = e.Message;
            while (e.InnerException != null)
            {
                e = e.InnerException;
                errorMessage = errorMessage + "\n" + e.Message;
            }
            return errorMessage;
        }

        private string CallYumlyApi(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                var htttpResponse = client.GetAsync(requestUrl);
                htttpResponse.Wait();
                var htttpResult = htttpResponse.Result;
                return htttpResult.Content.ReadAsStringAsync().Result;
            }
        }

        private void ParseSearchResultsResponseAndStoreInDb(string httpResponse)
        {
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
        }

        public void ParseGetResultsResponseAndUpdateInDb(string httpResponse, RecipeSearchResult recipeSearchResult)
        {
            JObject jObect = JObject.Parse(httpResponse);
            recipeSearchResult.Ingredients = jObect["ingredientLines"].ToString();
            recipeSearchResult.RecipeSourceUrl = jObect["source"]["sourceRecipeUrl"].ToString();
            db.SaveChanges();
        }

        public ActionResult Details(string id)
        {
            try
            {
                RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
                if (recipeSearchResult.Ingredients == null)
                {
                    string _requestUrl =
                        "api/recipe/" + recipeSearchResult.Id + "?_app_id=" + AppId + "&_app_key=" + AppKey;
                    string httpResponse = CallYumlyApi(_requestUrl);
                    ParseGetResultsResponseAndUpdateInDb(httpResponse, recipeSearchResult);
                }
                return View(recipeSearchResult);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = GetErrorMessage(e);
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
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

        public ActionResult SaveRecipe(String id)
        {
            RecipeSearchResult result = db.RecipeSearchResults.Find(id);
            if (result != null)
            {
                UserRecipe userRecipe = db.UserRecipes.Find(result.Id, User.Identity.Name);
                if (userRecipe == null)
                {
                    userRecipe = new UserRecipe();
                    userRecipe.RecipeId = result.Id;
                    userRecipe.RecipeName = result.RecipeName;
                    userRecipe.ImageUrl = result.ImageUrl;
                    userRecipe.RecipeSourceUrl = result.RecipeSourceUrl;
                    userRecipe.Ingredients = result.Ingredients;

                    userRecipe.UserName = User.Identity.Name;

                    db.UserRecipes.Add(userRecipe);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "UserRecipes");
        }
    }
}
