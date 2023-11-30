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
    public class VehiclesController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

        // GET: api/Vehicles
        public IHttpActionResult GetVehicles()
        {
            var veh = db.Vehicles.Select(v => new { v.VId, v.Name, v.VType, v.EntryCost, v.Capacity, v.ParkId }).ToList();
            return Ok(veh) ;
        }

        // GET: api/Vehicles/5
    
        public IHttpActionResult GetVehicle(int id)
        {
            var veh = db.Vehicles.Select(v => new { v.VId, v.Name, v.VType, v.EntryCost, v.Capacity, v.ParkId }).FirstOrDefault(v => v.VId==id);
            if (veh == null)
            {
                return NotFound();
            }

            return Ok(veh);
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            var veh = db.Vehicles.Find(id);
            if (veh == null)
            {
                return NotFound();
            }
            else
            {
                veh.Name = vehicle.Name;
                veh.VType=vehicle.VType;
                veh.EntryCost = vehicle.EntryCost;
                veh.Capacity=vehicle.Capacity;
                veh.ParkId=vehicle.ParkId;
                db.SaveChanges();
                return Ok();
            }


        }

        // POST: api/Vehicles
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.VId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.VId == id) > 0;
        }
    }
}