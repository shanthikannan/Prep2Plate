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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prep2Plate.Context;
using Prep2Plate.Models;

namespace Prep2Plate.Controllers
{
    public class RecipesSearchController : Controller
    {
        private RecipeContext db = new RecipeContext();        
        ////Hosted web API REST Service base url  
        private string AppId = "bf728886";
        private string AppKey = "a1608c3e5cc021f4354aa6755f42c294";
        private string Baseurl = "http://api.yummly.com/v1/";
        private string _requestUrl;
       
        // Function called before the Page is loaded
        public ActionResult Index(int? id) // Optional Parameter id. id will be null if not passed from the calling location.
        {
            if (id.HasValue)//if id has value, do nothing. 
            {
            } else { //else clear the database.
                ClearDatabase();
            }
            //return the view (Index.cshtml) with the model from the database.
            return View(db.RecipeSearchResults.ToList());
        }

        //This Action Method will be called when the user clicks the search button, 
        //searchRecipe string will be the value the user entered in the edit box
        public ActionResult OnSearchRecipe(string searchRecipe)
        {
            //Clear the database first. (So that the last search results are cleared).
            ClearDatabase();

            //Call the function to get the data from Ymmly Api backoffice and store it to Db.
            //THis function will return true, if successful and false otherwise.
            bool backendResult = GetDataFromYummyApiTask(searchRecipe);
            if (backendResult) //If the result is successful.. i.e. we got the value from yummly and stored it to the database
            {
                //Load the Index page (Recipe Search Page), so that it can display the search results. The pass a dummy id, so 
                //that we don't clear the database. Refer line number 31
                return RedirectToAction("Index", new { id = -1 });
            }
            else
            {
                //Show Error Message. For now showing the About screen.
                return RedirectToAction("About", "Home");
            }
        }

        private bool GetDataFromYummyApiTask(string searchString)
        {
            _requestUrl = "api/recipes?_app_id=" + AppId + "&_app_key=" + AppKey + "&requirePictures=true&q=" + searchString;
            using (var client = new HttpClient())
            {
                //Doing HTTP magic to get the data from Yummly Api
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to web api REST service using HttpClient  
                    var htttpResponse = client.GetAsync(_requestUrl);
                    htttpResponse.Wait();
                    var htttpResult = htttpResponse.Result;

                    //Return false if the request is NOT successful
                    if (!htttpResult.IsSuccessStatusCode)
                    {
                        return false;
                    }

                    //Getting the response result as string
                    string strResponse = htttpResult.Content.ReadAsStringAsync().Result;

                    //Convert the string to json
                    JObject jObect = JObject.Parse(strResponse);
                
                    //Get the json Array value for the key that has the name "matches"
                    IList<JToken> jTokenList = jObect["matches"].Children().ToList();
                
                    //For each json array element, iterate this loop (one array element represents one RecipeSearchResult)
                    foreach(JToken jToken in jTokenList)
                    {
                        //Create the recipe search object
                        RecipeSearchResult recipeSearchResult = new RecipeSearchResult
                        {
                            Id = jToken["id"].ToString(), //get the id
                            RecipeName = jToken["recipeName"].ToString(), //get the recipeName
                            ImageUrl = jToken["imageUrlsBySize"]["90"].ToString(), // get the imageUrl 
                            Ingredients = jToken["ingredients"].ToString() //get the ingredients
                        };

                        //Add the new recipeSearchResult to the dbSet in the RecipeContext
                        db.RecipeSearchResults.Add(recipeSearchResult);
                    }

                    //After adding all the recipeSearchResult objects to the dbSet, save the changes in the database
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    //In case of any exception, return false.
                    return false;
                }

            }
        }

        //This Action Method will be called when the user clicks an image from the recipe search result.
        //The id of the RecipeSearchResult model is being passed to this method from Index.cshtml
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // return the the recipe search result  (a row in the table whose id mathches with a id that is passed).
            RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
            if (recipeSearchResult == null)
            {
                return HttpNotFound();
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
        // POST: /Dinners/Edit/2

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit()
        { 
          
            return RedirectToAction("");
        }
    }
}
