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
    public class UsersController : ApiController
    {
        private JungleSafariEntities1 db = new JungleSafariEntities1();

        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            var users = db.Users.Select(u => new { u.UserId, u.UserName, u.Password, u.Name, u.RoleId, u.Role.roleName }).ToList();

            return Ok(users) ;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            var user = db.Users.Select(u => new { u.UserId, u.UserName, u.Password, u.Name, u.RoleId, u.Role.roleName }).FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            var u = db.Users.Find(id);
            if(u != null)
            {
                u.UserName = user.UserName;
                u.Password = user.Password;
                u.Name = user.Name;
                u.RoleId = user.RoleId;
                db.SaveChanges();
                return Ok();

            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        [AllowAnonymous]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}