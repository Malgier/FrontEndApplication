using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FrontEndApp.Models;

namespace FrontEndApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            Client client = new Client();
            PartialVM vm = client.GetClient("http://localhost:54997", "api/CustomerOrdering/View/Cart", "Cart Service Down");
            return View(vm);
        }
    }
}