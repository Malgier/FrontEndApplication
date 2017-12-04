using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using FrontEndApp.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FrontEndApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private string profileServiceLink;

        public ProfileController(IConfiguration config)
        {
            profileServiceLink = config.GetValue<string>("ProfileService");
        }

        // GET: Profile
        // /User/Index
        [HttpGet]
        [Route("User/Index")]
        public ActionResult Index()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(profileServiceLink, "/User/Index", cookievalue, "User Service Down");
            return View(vm);
        }

        // GET: User/Profile/5
        [HttpGet]
        [Route("User/Profile/{id}")]
        public ActionResult Profile(string id)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(profileServiceLink, "/User/Profile/" + id, cookievalue, "User Service Down");
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditProfilePost(User model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(profileServiceLink + "/User/EditProfilePost", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}