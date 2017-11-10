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
        static HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            client.BaseAddress = new Uri("http://localhost:54330");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            HttpResponseMessage response = client.GetAsync("/Products/Index").Result;

            if (response.IsSuccessStatusCode)
            {
                ProductVM vm = new ProductVM();
                vm.ProductsPartialView = response.Content.ReadAsStringAsync().Result;
                return View(vm);
            }
            else
            {
                return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
