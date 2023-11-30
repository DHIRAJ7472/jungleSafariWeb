
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApi.Infrastructure;
using webApi.Models;

namespace webApi.Controllers
{
    public class LoginController : ApiController
    { 
        JungleSafariEntities1 db = new JungleSafariEntities1();

        [Route("api/Login/{username}/{password}")]
        public IHttpActionResult GetLogin(string username,string password)
        {
            MyJwtToken token = TokenManager.GenerateToken(username, password);
            if (token == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(token);
            }


        }
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var u = db.Users.FirstOrDefault(us=>us.UserName == user.UserName);
            if (u != null)
            {
                return NotFound();
            }
            else
            {

                db.Users.Add(user);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
            }

        }
    }
}
