using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApi.Models;
using webApi.Infrastructure;

namespace webApi.Controllers
{
    [CustomAuthorizationFilter]
    public class CustomController : ApiController
    {
        JungleSafariEntities1 db = new JungleSafariEntities1();
        public IHttpActionResult GetBookingStatus()
        {
            var booking = db.Bookings.Select(b => new { b.Status, b.Id, b.SafariDetail.SafariName, b.SafariDetail.SafariDate, b.SafariDetail.SafariTime, b.Park.Name, b.Park.Location, b.Vehicle.VType, b.TotalCost }).ToList();/*Where(b => b.SafariDate > DateTime.Today.AddDays(-5));*/
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }
        public IHttpActionResult GetBookingStatus(int id)
        {
            var booking = db.Bookings.Select(b => new { b.Status,b.Id, b.SafariDetail.SafariName,b.SafariDetail.SafariDate,b.SafariDetail.SafariTime, b.Park.Name,b.Park.Location, b.Vehicle.VType,  b.TotalCost }).FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }
    }
}
