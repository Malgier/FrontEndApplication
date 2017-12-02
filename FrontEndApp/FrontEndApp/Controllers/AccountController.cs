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
            
            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient("https://localhost:44347/", "/Account/Login", cookievalue, "Login Service Down");
            return View(vm);
        }

        [HttpPost]
        [Route("Account/LoginReturn")]
        public IActionResult LoginReturn(LoginViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync("https://localhost:44347/Account/LoginReturn", model).Result;
                if(response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsAsync<TokenResponse>().Result;
                    Response.Cookies.Append("token", content.AccessToken);
                }
                return Ok();
            }
        }

        public IActionResult Register()
        {
            Client client = new Client();
            
            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient("https://localhost:44347/", "/Account/Register", cookievalue, "Register Service Down");
            return View(vm);
        }

        [HttpPost]
        [Route("Account/RegisterUser")]
        public IActionResult RegisterUser(RegisterViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync("https://localhost:44347/Account/RegisterUser", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsAsync<TokenResponse>().Result;
                    Response.Cookies.Append("token", content.AccessToken);
                }
                return Ok();
            }
        }
    }
}