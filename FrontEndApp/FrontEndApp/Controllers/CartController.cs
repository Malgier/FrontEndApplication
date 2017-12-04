using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FrontEndApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private string cartServiceLink;

        public CartController(IConfiguration config)
        {
            cartServiceLink = config.GetValue<string>("CartService");
        }

        [Route("Cart/Index")]
        public IActionResult Index()
        {
            Client client = new Client();
            
            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(cartServiceLink, "api/CustomerOrdering/View/Cart", cookievalue, "Cart Service Down");
            return View(vm);
        }
    }
}