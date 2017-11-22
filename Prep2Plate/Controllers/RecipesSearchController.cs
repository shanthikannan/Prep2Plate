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
        private string requestUrl;

        private List<RecipeSearchResult> GetDataFromYummyApiTask(string SearchString)
        {
            requestUrl = "api/recipes?_app_id=" + AppId + "&_app_key=" + AppKey + "&" + SearchString;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                var htttpResponse = client.GetAsync(requestUrl);
                htttpResponse.Wait();
                var htttpResult = htttpResponse.Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (!htttpResult.IsSuccessStatusCode)
                {
                    return null;
                }

                //Storing the response details recieved from web api   
                string strResponse = htttpResult.Content.ReadAsStringAsync().Result;
                JObject jObect = JObject.Parse(strResponse);
                IList<JToken> jTokenList = jObect["matches"].Children().ToList();
                
                // serialize JSON results into .NET objects
                IList<RecipeSearchResult> recipeResults = new List<RecipeSearchResult>();
                foreach(JToken jToken in jTokenList)
                {
                    RecipeSearchResult recipeSearchResult = new RecipeSearchResult
                    {
                        RecipeName = jToken["recipeName"].ToString(),
                        ImageUrl = jToken["imageUrlsBySize"]["90"].ToString()
                    };
                    recipeResults.Add(recipeSearchResult);
                }
                return recipeResults.ToList();
                //returning the employee list to view  
            }
        }

        // GET: RecipeSearchResults
        public ActionResult Index()
        {
            return View(db.RecipeSearchResults.ToList());
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
            return View(recipeSearchResult);
        }

        public ActionResult OnSearchRecipe(string recipeSearch)
        {
            //Get the value of the text field
            //Make web api call
            //Update View
            recipeSearch = "chicken";
            List<RecipeSearchResult> recipeSearchResults = new List<RecipeSearchResult>();
            recipeSearchResults = GetDataFromYummyApiTask(recipeSearch);
            if (recipeSearchResults == null)
            {
                return RedirectToAction("Create");
                //Show Error Message
            }
            else
            {
                var vm = recipeSearchResults[0];
                return View(vm);
            }
        }

        // GET: RecipeSearchResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeSearchResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImageUrl,RecipeName")] RecipeSearchResult recipeSearchResult)
        {
            if (ModelState.IsValid)
            {
                db.RecipeSearchResults.Add(recipeSearchResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipeSearchResult);
        }

        // GET: RecipeSearchResults/Edit/5
        public ActionResult Edit(string id)
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
            return View(recipeSearchResult);
        }

        // POST: RecipeSearchResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageUrl,RecipeName")] RecipeSearchResult recipeSearchResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipeSearchResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipeSearchResult);
        }

        // GET: RecipeSearchResults/Delete/5
        public ActionResult Delete(string id)
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
            return View(recipeSearchResult);
        }

        // POST: RecipeSearchResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RecipeSearchResult recipeSearchResult = db.RecipeSearchResults.Find(id);
            db.RecipeSearchResults.Remove(recipeSearchResult);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
