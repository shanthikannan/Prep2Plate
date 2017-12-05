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
    [Authorize] //This line ensure the page is acessable only after user is logged in.
                //If the user has not logged in. 
                //The login page will be diplayed automatically

    public class RecipesSearchController : Controller
    {
        // Creating instance of the db context class to access the model.
        private ApplicationDbContext db = new ApplicationDbContext();        
        
        // Credential to access yummily api
        private string AppId = "bf728886";
        private string AppKey = "a1608c3e5cc021f4354aa6755f42c294";
        private string Baseurl = "http://api.yummly.com/v1/";

        /* This action method will be called whenever the serach recipe is loaded. 
         * int? id is a optional parameter that is used to prevent clearing the db.
         * Optional Parameter id.id will be null if not passed from the calling location.
         */
        public ActionResult Index(int? id)
        {
            if (id.HasValue)//if id has value, do nothing. 
            {
            }
            else
            { //else clear the database.
                ClearDatabase();
            }
            //return the view (Index.cshtml) with the model from the database. We are using Db context variable
            //to get the recipe search results from the database.
           return View(db.RecipeSearchResults.ToList());
        }

        //This Action Method will be called when the user clicks the search button, 
        //searchRecipe string will be the value the user entered in the edit box
        public ActionResult OnSearchRecipe(string searchRecipe)
        {         
            //Clear the database first. (So that the last search results are cleared).
            ClearDatabase();

            //Building requestUrl to call the yummily api
            string _requestUrl = "api/recipes?_app_id=" + AppId + "&_app_key=" + AppKey + "&requirePictures=true&q=" + searchRecipe;

            // Call the function "CallYumlyApi" to make the HTTP REST API call which returns the HTTP Response in string
            string httpResponse = CallYumlyApi(_requestUrl);

            //Call the fn "ParseSearchResultsResponseAndStoreInDb" to Parse the search result HTTPresponse string 
            //and store the data(search result) in db. This fn returns a boolean (True if sucessfull and false otherwise)
            bool result = ParseSearchResultsResponseAndStoreInDb(httpResponse);
            if (result) // if result is tr ue load index.csthmlpage
            {   // We pass an id value (this will prevent clearing the db)
                return RedirectToAction("Index", new { id = -1 });
            }
            else
            { // we need to fix to show error msg
                return RedirectToAction("About", "Home");
            }
        }
        // Fn to call yumly api web service(this fn is used to call both search recipe and get recipe api calls).
        // requestUrl is the query string. Returns the http response.
        private string CallYumlyApi(string requestUrl)
        {
            // Create a new http client object. "Using" is used to make the block of code run only if http client object
            //creation is sucessful.
            using (var client = new HttpClient())
            {   // Setting the baseaddress property for http client
                // The BaseAddress prop is an object of type Uri, so We are creating a new Uri object
                client.BaseAddress = new Uri(Baseurl);
                
                // Clearing default http headers
                client.DefaultRequestHeaders.Clear();
                
                // Setting response type as json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //Sending request to the REST service web Api
                    //
                    var htttpResponse = client.GetAsync(requestUrl);
                    
                    // Wait for the response. The program execution will be blocked until we recive a response
                    htttpResponse.Wait();

                    // Get response and store it in htttpResult variable
                    var htttpResult = htttpResponse.Result;

                    //Checking if the response is successful or not.
                    if (!htttpResult.IsSuccessStatusCode)
                    {
                        return null;// if not sucessful return null
                    }

                    // Convert htttpResult as string and return it.
                    return htttpResult.Content.ReadAsStringAsync().Result;
                } 
                catch (Exception)
                {// catch any exception and return null
                    return null;
                }
            }
        }
        // Convert the httpResponse string  to json and parse the json response and create RecipeSearchResult model(s).
        // The method updates the recipe Id, recipe Name and the Image url of each of the RecipeSearchResult.
        //Using db context store RecipeSearchResult(s) to db. 
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

        // Convert the httpResponse string  to json and parse the json response and update the current 
        //RecipeSearchResult model with Ingredients and the Source Url.
        //Using db context store the current RecipeSearchResult to db. 
        public bool ParseGetResultsResponseAndUpdateInDb(string httpResponse, RecipeSearchResult recipeSearchResult)
        {
            JObject jObect = JObject.Parse(httpResponse);
            recipeSearchResult.Ingredients = jObect["ingredientLines"].ToString();
            recipeSearchResult.RecipeSourceUrl = jObect["source"]["sourceRecipeUrl"].ToString();
            db.SaveChanges();
            return false;
        }

        // this action method will be called when user clicks the image or recipe name.
        // this method calls the get recipe api call and parse the response and update the Db.
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

        //Clears the Database.
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

        //This Api Method will be called whe the user clicks Save recipe button. 
        //This method reads the select recipe details from the RecipeSearchResult database and stores
        //the data in the UserRecipes Table. (of course, using Db Context). Then it redirects the View 
        //to the My Recipes Page.
        public ActionResult SaveRecipe(String id)
        { 
            RecipeSearchResult result = db.RecipeSearchResults.Find(id);
            UserRecipe userRecipe = db.UserRecipes.Find(result.Id, User.Identity.Name);
            if (userRecipe == null)
            {
                userRecipe = new UserRecipe();
                userRecipe.RecipeId = result.Id;
                userRecipe.RecipeName = result.RecipeName;
                userRecipe.ImageUrl = result.ImageUrl;
                userRecipe.RecipeSourceUrl s = result.RecipeSourceUrl;
                userRecipe.Ingredients = result.Ingredients;

                userRecipe.UserName = User.Identity.Name;

                db.UserRecipes.Add(userRecipe);
                db.SaveChanges();
            }
            return RedirectToAction("Index","UserRecipes");
        }
    }
}
