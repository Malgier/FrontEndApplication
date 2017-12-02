using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEndApp.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace FrontEndApp.Controllers
{
    public class AccountController : Controller
    {
        private string accountServiceLink;

        public AccountController(IConfiguration config)
        {
            accountServiceLink = config.GetValue<string>("AuthService");
        }

        [HttpGet]
        public IActionResult Login()
        {
            Client client = new Client();
            
            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }
            
            PartialVM vm = client.GetClient(accountServiceLink, "/Account/Login", cookievalue, "Login Service Down");
            return View(vm);
        }

        [HttpPost]
        [Route("Account/LoginReturn")]
        public IActionResult LoginReturn(LoginViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(accountServiceLink + "/Account/LoginReturn", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsAsync<TokenResponse>().Result;
                    Response.Cookies.Append("access_token", content.AccessToken);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        public IActionResult Register()
        {
            Client client = new Client();
            
            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(accountServiceLink, "/Account/Register", cookievalue, "Register Service Down");
            return View(vm);
        }

        [HttpPost]
        [Route("Account/RegisterUser")]
        public IActionResult RegisterUser(RegisterViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(accountServiceLink + "/Account/RegisterUser", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsAsync<TokenResponse>().Result;
                    Response.Cookies.Append("token", content.AccessToken);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}