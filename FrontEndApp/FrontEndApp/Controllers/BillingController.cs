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
    public class BillingController : Controller
    {
        HttpMessageHandler _handler;
        private string billingServiceLink;

        public BillingController(IConfiguration config, HttpMessageHandler handler = null)
        {
            _handler = handler == null ? new HttpClientHandler() : handler;
            billingServiceLink = config.GetValue<string>("BillingService");
        }

        [Route("Billing/AddCard")]
        public IActionResult AddCard()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(billingServiceLink, "api/Card/AddCard", cookievalue, "Billing Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [HttpPost]
        [Route("Billing/AddCartPost")]
        public IActionResult AddCartPost(Card model)
        {
            using (HttpClient client = new HttpClient(_handler, false))
            {
                var response = client.PostAsJsonAsync(billingServiceLink + "/Card/AddCartPost", model).Result;
                if (response.IsSuccessStatusCode)
                {

                    if (User.IsInRole("Staff") || User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else if (User.IsInRole("Customer"))
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [Route("Billing/Finalise")]
        public IActionResult Finalise()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(billingServiceLink, "Card/Finalise", cookievalue, "Billing Service Down", _handler);
            return View("Shared/Simple", vm);
        }
    }
}