using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEndApp.Models;
using System.Net.Http;

namespace FrontEndApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            Client client = new Client();
            PartialVM vm = client.GetClient("https://localhost:44347/", "/Account/Login");

            //Read cookie
            string cookievalue;
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            return View(vm);
        }

        [HttpPost]
        [Route("Account/Login")]
        public IActionResult LoginReturn([FromBody] LoginViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync("https://localhost:44347/Account/Login", model).Result;
                if(response.IsSuccessStatusCode)
                {
                    //var conent = response.Content.ReadAsAsync()
                }
                return Ok();
            }
        }

        public IActionResult Register()
        {
            Client client = new Client();
            PartialVM vm = client.GetClient("https://localhost:44347/", "/Account/Register");
            return View(vm);
        }
    }
}