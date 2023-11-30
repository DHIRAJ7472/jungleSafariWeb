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
    public class VehiclesController : Controller
    {
        

        // GET: Vehicles
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
        public ActionResult IndexUp(int id)
        {
            List<VehicleView> vList = new List<VehicleView>();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/vehicles").Result;
            if (response.IsSuccessStatusCode)
            {
                vList = response.Content.ReadAsAsync<List<VehicleView>>().Result;
            }
            var vehicle = from v in vList where v.ParkId==id select v;
            return View(vehicle);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            VehicleView vehicle = new VehicleView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/vehicles/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                vehicle = response.Content.ReadAsAsync<VehicleView>().Result;
            }
        
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create(int id)
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
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");

            VehicleView vehicleView = new VehicleView();
            vehicleView.ParkId = id;
            //gateView.ParkId = id;

            return View(vehicleView);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VId,VType,Name,EntryCost,Capacity,ParkId")] VehicleView vehicle)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PostAsJsonAsync("https://localhost:44322/api/vehicles/", vehicle).Result;

            if (Response.IsSuccessStatusCode)
            {

                return RedirectToAction("IndexUp", new {id=vehicle.ParkId});
            }


            List<ParkView> parkList = new List<ParkView>();

            var response1 = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
            return View(vehicle);

   
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            VehicleView v = new VehicleView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/vehicles/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                v = response.Content.ReadAsAsync<VehicleView>().Result;
            }
            List<ParkView> parkList = new List<ParkView>();

            var response1 = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
          
            return View(v);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VId,VType,Name,EntryCost,Capacity,ParkId")] VehicleView vehicle)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PutAsJsonAsync("https://localhost:44322/api/vehicles/"+vehicle.VId, vehicle).Result;

            if (Response.IsSuccessStatusCode)
            {

                return RedirectToAction("IndexUp", new { id = vehicle.ParkId });
            }

            List<ParkView> parkList = new List<ParkView>();

            var response1 = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            VehicleView vehicle = new VehicleView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/vehicles/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                vehicle = response.Content.ReadAsAsync<VehicleView>().Result;
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ParkId, int VId)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responce = client.DeleteAsync("https://localhost:44322/api/vehicles/" + VId).Result;
            return RedirectToAction("IndexUp", new {id=ParkId});
        }

       
    }
}
