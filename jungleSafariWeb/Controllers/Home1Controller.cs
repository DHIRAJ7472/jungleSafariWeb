using jungleSafariWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using jungleSafariWeb.ViewModel;
using System.Web.Security;
using System.Web.UI.WebControls;
using webApi.Infrastructure;

namespace jungleSafariWeb.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Home1
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersView user)
        {
            List<UsersView> users = new List<UsersView>();
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:44322/api/Login/"+user.UserName +"/"+user.Password).Result;

            if (response.IsSuccessStatusCode)
            {

                MyJwtToken token = response.Content.ReadAsAsync<MyJwtToken>().Result;
                Response.Cookies["jwtToken"].Value = token.Token;
                Response.Cookies["rolename"].Value = token.RoleName;
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("BookSafari", "Bookings");
            }

            else
            {
                ViewBag.ErroMessage = "invalid username or passord";
                return View();
            }


        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UsersView user)
        {
            HttpClient client = new HttpClient();
            var response = client.PostAsJsonAsync("https://localhost:44322/api/Login/",user).Result;
           
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");

            }
            else
            {
                ViewBag.ErroMessage = "* Username already present";
                return View();

            }
        
        }
        public ActionResult Logout()
        {
            Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Home", "Home1");
        }
    }
}