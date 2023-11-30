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
    public class GatesController : Controller
    {
        

        // GET: Gates
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
            HttpClient client = new HttpClient();
            List<GateView> gateList = new List<GateView>();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = client.GetAsync("https://localhost:44322/api/gates").Result;
            if (response1.IsSuccessStatusCode)
            {
                gateList = response1.Content.ReadAsAsync<List<GateView>>().Result;
            }
            var g = from gate in gateList where gate.ParkId == 101 select gate;
            return View(g);
            
        }

        // GET: Gates/Details/5
        public ActionResult Details(int? id)
        {
            GateView gate = new GateView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/gates/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                gate = response.Content.ReadAsAsync<GateView>().Result;
            }
            return View(gate);
           
        }

        // GET: Gates/Create
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            GateView gateView = new GateView();
            gateView.ParkId=id;
            return View(gateView);
        }

        // POST: Gates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GateId,Name,ParkId")] GateView gate)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PostAsJsonAsync("https://localhost:44322/api/gates/", gate).Result;

            if (Response.IsSuccessStatusCode)
            {

                return RedirectToAction("IndexUp", new {id=gate.ParkId});
            }

         
            List<ParkView> parkList = new List<ParkView>();
           
            var response1 = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
            return View(gate);
        }

        // GET: Gates/Edit/5
        public ActionResult Edit(int? id)
        {
            GateView gate = new GateView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/gates/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                gate = response.Content.ReadAsAsync<GateView>().Result;
            }
            List<ParkView> parkList = new List<ParkView>();

            var response1 = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
            return View(gate);
        }

        // POST: Gates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GateId,Name,ParkId")] GateView gate)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Response = client.PutAsJsonAsync("https://localhost:44322/api/gates/"+gate.GateId, gate).Result;

            if (Response.IsSuccessStatusCode)
            {

                return RedirectToAction("IndexUp", new {id=gate.ParkId});
            }

            List<ParkView> parkList = new List<ParkView>();

            var response1 = client.GetAsync("https://localhost:44322/api/parks").Result;
            if (response1.IsSuccessStatusCode)
            {
                parkList = response1.Content.ReadAsAsync<List<ParkView>>().Result;
            }
            ViewBag.ParkId = new SelectList(parkList, "ParkId", "Name");
            return View(gate);
        }

        // GET: Gates/Delete/5
        public ActionResult Delete(int? id)
        {
            GateView gate = new GateView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/gates/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                gate = response.Content.ReadAsAsync<GateView>().Result;
            }
            return View(gate);
        }

        // POST: Gates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int GateId, int ParkId)
        {
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responce = client.DeleteAsync("https://localhost:44322/api/gates/" + GateId).Result;
            return RedirectToAction("IndexUp", new {id=ParkId});
        }


    }
}
