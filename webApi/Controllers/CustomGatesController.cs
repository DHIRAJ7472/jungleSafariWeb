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
    public class CustomGatesController : ApiController
    {
        JungleSafariEntities1 db = new JungleSafariEntities1();
        public IHttpActionResult Get(int id)
        {
            List<Gate> gates = new List<Gate>();
            gates = db.Gates.Where(g => g.ParkId==id).ToList();
            return Ok(gates);
        }
    }
}
