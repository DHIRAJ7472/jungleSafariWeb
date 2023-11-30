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
    public class SafariDetailsController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

        // GET: api/SafariDetails
        public IHttpActionResult GetSafariDetails()
        {
            var safariList = db.SafariDetails.Select(s => new { s.SafariId, s.SafariName, s.SafariDate,s.SafariTime,s.ParkId,s.SafariCost,s.Park.Name,s.Park.Location,s.Park.Fee }).ToList();
            return Ok(safariList);
        }

        // GET: api/SafariDetails/5

        
        public IHttpActionResult GetSafariDetail(int id)
        {
            var safari = db.SafariDetails.Select(s => new { s.SafariId, s.SafariName, s.SafariDate, s.SafariTime, s.ParkId, s.SafariCost, s.Park.Location, s.Park.Fee }).FirstOrDefault(s =>s.SafariId==id);
            if (safari == null)
            {
                return NotFound();
            }

            return Ok(safari);
        }

        // PUT: api/SafariDetails/5
        [Authorize(Roles = "Admin")]

        public IHttpActionResult PutSafariDetail(int id, SafariDetail safariDetail)
        {
            var s = db.SafariDetails.Find(id);
            if (s != null)
            {
                s.SafariId = safariDetail.SafariId;
                s.SafariName = safariDetail.SafariName;
                s.SafariDate = safariDetail.SafariDate;
                s.SafariTime = safariDetail.SafariTime;
                s.ParkId = safariDetail.ParkId;
                s.SafariCost= safariDetail.SafariCost;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }

           
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostSafariDetail(SafariDetail safariDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SafariDetails.Add(safariDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = safariDetail.SafariId }, safariDetail);
        }

        // DELETE: api/SafariDetails/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(SafariDetail))]
        public IHttpActionResult DeleteSafariDetail(int id)
        {
            SafariDetail safariDetail = db.SafariDetails.Find(id);
            if (safariDetail == null)
            {
                return NotFound();
            }

            db.SafariDetails.Remove(safariDetail);
            db.SaveChanges();

            return Ok(safariDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SafariDetailExists(int id)
        {
            return db.SafariDetails.Count(e => e.SafariId == id) > 0;
        }
    }
}