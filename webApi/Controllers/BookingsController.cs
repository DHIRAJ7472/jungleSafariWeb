using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webApi.Models;

using webApi.Infrastructure;

namespace webApi.Controllers
{
    [CustomAuthorizationFilter]
    public class BookingsController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

        // GET: api/Bookings
        public IHttpActionResult GetBookings()
        {
            var bookingsList = db.Bookings.Select(b => new {b.SafariId,b.GateId,b.VehicleId,b.PId,b.Status,b.Id,b.TotalCost}).OrderByDescending(comparer => comparer.Id).ToList();
            return Ok(bookingsList);
            
        }

        // GET: api/Bookings/5

        public IHttpActionResult GetBooking(int id)
        {
            var booking = db.Bookings.Select(b => new { b.SafariId, b.GateId, b.VehicleId, b.PId, b.Status, b.Id,b.TotalCost }).FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Bookings/5


        public IHttpActionResult PutBooking(int id, Booking booking)
        {
            var b = db.Bookings.Find(id);
            if (b != null)
            {
                b.SafariId=booking.SafariId;
                b.PId = booking.PId;
                b.Status=booking.Status;
                b.VehicleId=booking.VehicleId;
                b.GateId = booking.GateId;
                b.TotalCost=booking.TotalCost;

                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Bookings

        public IHttpActionResult PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bookings.Add(booking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5


        public IHttpActionResult DeleteBooking(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {

                return Content(HttpStatusCode.NotFound, "Booking Not Found");
            }
            else
            {
                var tList=db.Tourists.Where(t => t.BookingId == id).ToList();
                foreach(var t in tList)
                {
                    db.Tourists.Remove(t);
                }
                db.SaveChanges();
                db.Bookings.Remove(booking);
                db.SaveChanges();
                return Ok(booking);

            }   
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(int id)
        {
            return db.Bookings.Count(e => e.Id == id) > 0;
        }
    }
}