using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEndApp.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEndApp.Controllers
{
    public class HomeController : Controller
    {
        HttpMessageHandler _handler;
        private string productServiceLink;

        public HomeController(IConfiguration config, HttpMessageHandler handler = null)
        {
            _handler = handler == null ? new HttpClientHandler() : handler;
            productServiceLink = config.GetValue<string>("ProductService");
        }

        [Route("Products/Index")]
        public IActionResult Index()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(productServiceLink, "api/Product/Views/Index", cookievalue, "Products not Found", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("Products/ProductDetails")]
        public IActionResult ProductDetails(string EAN)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(productServiceLink, "api/Product/Views/ProductDetails?EAN=" + EAN, cookievalue, "Product Details Not Found", _handler);
            return View("Shared/Simple", vm);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
