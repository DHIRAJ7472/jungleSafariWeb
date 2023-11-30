using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using jungleSafariWeb.Models;
using jungleSafariWeb.ViewModel;

namespace jungleSafariWeb.Controllers
{
    [Authorize]
    public class ParksController : Controller
    {
        

        // GET: Parks
        public ActionResult Index()
        {
            List<ParkView> parkList = new List<ParkView>();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response.IsSuccessStatusCode)
            {
                parkList = response.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            return View(parkList);
      
        }

        // GET: Parks/Details/5
        public ActionResult Details(int? id)
        {
            ParkView parkList = new ParkView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/parks/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                parkList = response.Content.ReadAsAsync<ParkView>().Result;
            }
            return View(parkList);
        }

        // GET: Parks/Create
        public ActionResult Create()
        {
  
            return View();
        }

        // POST: Parks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParkId,Name,Location,Fee")] ParkView park)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PostAsJsonAsync("https://localhost:44322/api/parks/", park).Result;

            if (Response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }


            return View(park);
        }

        // GET: Parks/Edit/5
        public ActionResult Edit(int? id)
        {
            ParkView park = new ParkView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/parks/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                park = response.Content.ReadAsAsync<ParkView>().Result;
            }
            return View(park);
        }

        // POST: Parks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParkId,Name,Location,Fee")] ParkView park)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PutAsJsonAsync("https://localhost:44322/api/parks/"+park.ParkId, park).Result;
            try
            {
                if (Response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }

            }
            catch(Exception e)
            {
                throw e;
            }

          

            return View(park);
        }

        // GET: Parks/Delete/5
        public ActionResult Delete(int? id)
        {
            ParkView park = new ParkView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/parks/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                park = response.Content.ReadAsAsync<ParkView>().Result;
            }
            return View(park);
        }

        // POST: Parks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responce = client.DeleteAsync("https://localhost:44322/api/parks/" + id).Result;
            return RedirectToAction("Index");
        }

       
        
    }
}
