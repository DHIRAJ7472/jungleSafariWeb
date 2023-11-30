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
    public class ManageController : Controller
    {
       

        public ActionResult Details(int? id)
        {
            SafariDetailView safariList = new SafariDetailView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/safaridetails/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                safariList = response.Content.ReadAsAsync<SafariDetailView>().Result;
            }
            return View(safariList);

        }

        // GET: Manage/Create
        public ActionResult Create()
        {
            List<ParkView> parkList = new List<ParkView>();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/parks/" ).Result;
            if (response.IsSuccessStatusCode)
            {
                parkList = response.Content.ReadAsAsync<List<ParkView>>().Result;
            }

            string[] time = { "Morning", "Evening" };
            ViewBag.time = new SelectList(time);
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
            return View();
        }

        // POST: Manage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafariId,SafariName,SafariDate,SafariTime,ParkId,SafariCost")] SafariDetailView safariDetail)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PostAsJsonAsync("https://localhost:44322/api/safaridetails/", safariDetail).Result;

            if (Response.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index","Bookings");
            }
            List<ParkView> parkList = new List<ParkView>();
            var response = client.GetAsync("https://localhost:44322/api/parks/").Result;
            if (response.IsSuccessStatusCode)
            {
                parkList = response.Content.ReadAsAsync<List<ParkView>>().Result;
            }

            

            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name", safariDetail.ParkId);
            return View(safariDetail);
        }

        // GET: Manage/Edit/5
        public ActionResult Edit(int? id)
        {
            SafariDetailView safariDetail = new SafariDetailView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/safaridetails/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                safariDetail = response.Content.ReadAsAsync<SafariDetailView>().Result;
            }

            List<ParkView> parkList = new List<ParkView>();
            var response1 = client.GetAsync("https://localhost:44322/api/parks/").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }



            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name", safariDetail.ParkId);
            return View(safariDetail);
        }

        // POST: Manage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafariId,SafariName,SafariDate,SafariTime,ParkId,SafariCost")] SafariDetailView safariDetail)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PutAsJsonAsync("https://localhost:44322/api/safaridetails/"+safariDetail.SafariId, safariDetail).Result;

            if (Response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "Bookings");
            }


            List<ParkView> parkList = new List<ParkView>();
            var response1 = client.GetAsync("https://localhost:44322/api/parks/").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }



            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name", safariDetail.ParkId);
            return View(safariDetail);
        }

        // GET: Manage/Delete/5
        public ActionResult Delete(int? id)
        {
            SafariDetailView safariDetail = new SafariDetailView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/safaridetails/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                safariDetail = response.Content.ReadAsAsync<SafariDetailView>().Result;
            }

            return View(safariDetail);
        }

        // POST: Manage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responce = client.DeleteAsync("https://localhost:44322/api/safaridetails/" + id).Result;
            return RedirectToAction("Index", "Bookings");


            
        }

        public ActionResult ManageSafaries()
        {
            return View();
        }

        
    }
}
