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
    public class GatesController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

        // GET: api/Gates
        public IHttpActionResult GetGates()
        {
            var gates = db.Gates.Select(g => new { g.GateId, g.ParkId, g.Name }).ToList();
            return Ok( gates);
        }

        // GET: api/Gates/5
    
        public IHttpActionResult GetGate(int id)
        {
            var gate = db.Gates.Select(g => new { g.GateId, g.ParkId, g.Name }).FirstOrDefault(g=>g.GateId==id);
            if (gate == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(gate);
            }

        }

        // PUT: api/Gates/5

        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutGate(int id, Gate gate)
        {
            var g = db.Gates.Find(id);
            if(g== null)
            {
                return NotFound();
            }
            else
            {
                
                g.ParkId = gate.ParkId;
                g.Name = gate.Name;
                db.SaveChanges();
                return Ok();
            }

        }

        // POST: api/Gates
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostGate(Gate gate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gates.Add(gate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gate.GateId }, gate);
        }

        // DELETE: api/Gates/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Gate))]
        public IHttpActionResult DeleteGate(int id)
        {
            Gate gate = db.Gates.Find(id);
            if (gate == null)
            {
                return NotFound();
            }

            db.Gates.Remove(gate);
            db.SaveChanges();

            return Ok(gate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GateExists(int id)
        {
            return db.Gates.Count(e => e.GateId == id) > 0;
        }
    }
}