using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEndApp.Models;
using System.Net.Http;

namespace FrontEndApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("Products/Index")]
        public IActionResult Index()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient("http://localhost:54330", "/Products/Index",cookievalue, "Products not Found");
            return View(vm);
        }

        [Route("Products/ProductDetails")]
        public IActionResult ProductDetails(string EAN)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient("http://localhost:54330", "/Products/ProductDetails?EAN=" + EAN, cookievalue, "Product Details Not Found");
            return View(vm);
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
