using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using jungleSafariWeb.Models;
using jungleSafariWeb.ViewModel;

namespace jungleSafariWeb.Controllers
{
    [HandleError]

    [Authorize]
    public class BookingsController : Controller
    {
       

        public ActionResult BookSafari()
        {

            List<SafariDetailView> safariList = new List<SafariDetailView>();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/safaridetails").Result;
            if (response.IsSuccessStatusCode)
            {
                safariList = response.Content.ReadAsAsync<List<SafariDetailView>>().Result;
            }
            return View(safariList);


        }

        public ActionResult Book(int ParkId, int SafariId)
        {
            ViewBag.ParkId = ParkId;
            ViewBag.SafariId = SafariId;
            ParkView park = new ParkView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/parks/"+ParkId).Result;
            if (response.IsSuccessStatusCode)
            {
                park = response.Content.ReadAsAsync<ParkView>().Result;
            }
            ViewBag.Location = park.Location;
            ViewBag.ParkName = park.Name;

            SafariDetailView sdf = new SafariDetailView();
            var response1 = client.GetAsync("https://localhost:44322/api/safaridetails/" + SafariId).Result;
            if (response1.IsSuccessStatusCode)
            {
                sdf = response1.Content.ReadAsAsync<SafariDetailView>().Result;
            }
            ViewBag.SafariName = sdf.SafariName;

            List<VehicleView> vehicleList = new List<VehicleView>();
            

            
            var response2 = client.GetAsync("https://localhost:44322/api/vehicles").Result;
            if (response2.IsSuccessStatusCode)
            {
                vehicleList = response2.Content.ReadAsAsync<List<VehicleView>>().Result;
            }
            var v = from vehicle in vehicleList where vehicle.ParkId == ParkId select vehicle;

            List<GateView> gateList = new List<GateView>();
            var response3 = client.GetAsync("https://localhost:44322/api/gates").Result;
            if (response3.IsSuccessStatusCode)
            {
                gateList = response3.Content.ReadAsAsync<List<GateView>>().Result;
            }
            var g = from gate in gateList where gate.ParkId == ParkId select gate;


            ParkVehicleGate pvg = new ParkVehicleGate();
            pvg.Vehicles = v.ToList();
            pvg.Gates = g.ToList();


          
            ViewBag.VIdList = v.ToList();
            ViewBag.GateIdList = g.ToList();

            return View(pvg);

        }


        public ActionResult Booking(int SafariId, int ParkId, int GateId, int VehicleId)
        {
            BookingView b = new BookingView();
            b.SafariId = SafariId;
            b.PId = ParkId;
            b.GateId = GateId;
            b.VehicleId = VehicleId;
            b.Status = "Confirm";

            VehicleView vehicle = new VehicleView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/vehicles/" + VehicleId).Result;
            if (response.IsSuccessStatusCode)
            {
                vehicle = response.Content.ReadAsAsync<VehicleView>().Result;
            }

            ParkView park = new ParkView();

            var response1 = client.GetAsync("https://localhost:44322/api/parks/" + ParkId).Result;
            if (response1.IsSuccessStatusCode)
            {
                park = response1.Content.ReadAsAsync<ParkView>().Result;
            }

            SafariDetailView safari = new SafariDetailView();
            var response2 = client.GetAsync("https://localhost:44322/api/safaridetails/" + SafariId).Result;
            if (response2.IsSuccessStatusCode)
            {
                safari = response2.Content.ReadAsAsync<SafariDetailView>().Result;
            }


            var vehicalCost = vehicle.EntryCost;
            var parkFee = park.Fee;
            var safariFee = safari.SafariCost;
            b.TotalCost = Convert.ToInt32(vehicalCost) + Convert.ToInt32(parkFee) + Convert.ToInt32(safariFee);

            BookingView bookView = null;
            var response3 = client.PostAsJsonAsync("https://localhost:44322/api/bookings", b).Result;
            if (response3.IsSuccessStatusCode)
            {

                bookView = response3.Content.ReadAsAsync<BookingView>().Result;
            }

            var bookingId = bookView.Id;
            ViewBag.BookingId = bookingId;

            return View();
        }

        [HttpGet]
        public ActionResult Tourist(int NoOfTourist, int BookingNo)
        {
           jungleSafariWeb.ViewModel.Tourists tourists = new jungleSafariWeb.ViewModel.Tourists();
            ViewBag.NoOfTourist = NoOfTourist;
            ViewBag.BookingNo = BookingNo;
            return View(tourists);
        }
        [HttpPost]
        public ActionResult Tourist(jungleSafariWeb.ViewModel.Tourists tourists)
        {


            try
            {
                foreach (var t in tourists.TouristsList)
                {
                    TouristView tourist = new TouristView();
                    tourist.Id = t.Id;
                    tourist.Name = t.Name;
                    tourist.Gender = t.Gender;
                    tourist.DateOfBirth = t.DateOfBirth;
                    tourist.MobileNo = t.MobileNo.ToString();
                    tourist.City = t.City;
                    tourist.Country = t.Country;
                    tourist.EmailId = t.EmailId;
                    tourist.IdentityName = t.IdentityName;
                    tourist.IdentityNumber = t.IdentityNumber;
                    tourist.BookingId = t.BookingId;

                    HttpClient client = new HttpClient();
                    TouristView touristView = null;

                    string token = Request.Cookies["jwtToken"].Value;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.PostAsJsonAsync("https://localhost:44322/api/tourists", t).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        touristView = response.Content.ReadAsAsync<TouristView>().Result;
                    }
                }
                return RedirectToAction("BookingStatus", new { id = tourists.TouristsList[0].BookingId });
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult BookingStatus(int id)
        {
            BookingStatusView bookingView = new BookingStatusView();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/custom/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                bookingView = response.Content.ReadAsAsync<BookingStatusView>().Result;
            }
            return View(bookingView);
        }
        public ActionResult CheckBookingStatus()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<SafariDetailView> safariList = new List<SafariDetailView>();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/safaridetails").Result;
            if (response.IsSuccessStatusCode)
            {
                safariList = response.Content.ReadAsAsync<List<SafariDetailView>>().Result;
            }
            return View(safariList);
           
        }  
        public ActionResult BookingsList()
        {

            List<BookingStatusView> ListbookingView = new List<BookingStatusView>();
            HttpClient client = new HttpClient();

            string token = Request.Cookies["jwtToken"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44322/api/custom/").Result;
            if (response.IsSuccessStatusCode)
            {
                ListbookingView = response.Content.ReadAsAsync<List<BookingStatusView>>().Result;
            }
            
            var bookings = from booking in ListbookingView where booking.SafariDate >= DateTime.Now.AddDays(-5) select booking;


            return View(bookings);
        }
    }
}