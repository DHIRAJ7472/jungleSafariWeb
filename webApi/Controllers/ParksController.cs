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
    public class ParksController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

       
        public IHttpActionResult GetParks()
        {
            var parkList= db.Parks.Select(p => new { p.ParkId, p.Name, p.Location, p.Fee }).ToList();
            return Ok(parkList);
        }

        // GET: api/Parks/5

        public IHttpActionResult GetPark(int id)
        {
            var park = db.Parks.Select(p => new { p.ParkId, p.Name, p.Location, p.Fee }).FirstOrDefault(p => p.ParkId == id);
            if (park == null)
            {
                return NotFound();
            }

            return Ok(park);
        }

        // PUT: api/Parks/5
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutPark(int id, Park park)
        {
            var p = db.Parks.Find(id);
            if (p != null)
            {
                p.Name = park.Name;
                p.Location = park.Location;
                p.Fee = park.Fee;
  
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Parks
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostPark(Park park)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parks.Add(park);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = park.ParkId }, park);
        }
        [Authorize(Roles = "Admin")]
        // DELETE: api/Parks/5
        [ResponseType(typeof(Park))]
        public IHttpActionResult DeletePark(int id)
        {
            Park park = db.Parks.Find(id);
            if (park == null)
            {
                return NotFound();
            }

            db.Parks.Remove(park);
            db.SaveChanges();

            return Ok(park);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParkExists(int id)
        {
            return db.Parks.Count(e => e.ParkId == id) > 0;
        }
    }
}