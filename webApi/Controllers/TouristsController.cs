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
    public class TouristsController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

        // GET: api/Tourists
        public IHttpActionResult GetTourists()
        {
            var tourists = db.Tourists.Select(t => new { t.Id, t.Name, t.Gender, t.DateOfBirth, t.MobileNo, t.City, t.Country, t.EmailId, t.IdentityName, t.IdentityNumber, t.BookingId }).ToList();
            return Ok(tourists) ;
        }

        // GET: api/Tourists/5

        public IHttpActionResult GetTourist(int id)
        {
            var tourist = db.Tourists.Select(t => new { t.Id, t.Name, t.Gender, t.DateOfBirth, t.MobileNo, t.City, t.Country, t.EmailId, t.IdentityName, t.IdentityNumber, t.BookingId }).FirstOrDefault(t => t.Id==id);
            if (tourist == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tourist);
            }

        }

        // PUT: api/Tourists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTourist(int id, Tourist tourist)
        {
            var t = db.Tourists.Find(id);
            if(t== null)
            {
                return NotFound();
            }
            else
            {
                t.Name = tourist.Name;
                t.Gender = tourist.Gender;
                t.DateOfBirth = tourist.DateOfBirth;
                t.MobileNo = tourist.MobileNo;
                t.City = tourist.City;
                t.Country = tourist.Country;
                t.EmailId = tourist.EmailId;
                t.IdentityName = tourist.IdentityName;
                t.IdentityNumber = tourist.IdentityNumber;
                t.BookingId = tourist.BookingId;
                db.SaveChanges();
                return Ok();
            }
        }

        // POST: api/Tourists
        [ResponseType(typeof(Tourist))]
        public IHttpActionResult PostTourist(Tourist tourist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tourists.Add(tourist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tourist.Id }, tourist);
        }

        // DELETE: api/Tourists/5
        [ResponseType(typeof(Tourist))]
        public IHttpActionResult DeleteTourist(int id)
        {
            Tourist tourist = db.Tourists.Find(id);
            if (tourist == null)
            {
                return NotFound();
            }

            db.Tourists.Remove(tourist);
            db.SaveChanges();

            return Ok(tourist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TouristExists(int id)
        {
            return db.Tourists.Count(e => e.Id == id) > 0;
        }
    }
}