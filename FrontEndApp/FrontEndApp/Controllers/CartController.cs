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
        HttpMessageHandler _handler;
        private string cartServiceLink;

        public CartController(IConfiguration config, HttpMessageHandler handler = null)
        {
            _handler = handler == null ? new HttpClientHandler() : handler;
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

            PartialVM vm = client.GetClient(cartServiceLink, "api/CustomerOrdering/View/Cart", cookievalue, "Cart Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("MyOrders")]
        public IActionResult MyOrders()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(cartServiceLink, "api/CustomerOrdering/View/Orders", cookievalue, "Order Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("Order/{orderId}")]
        public IActionResult Order(int orderId)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(cartServiceLink, "api/CustomerOrdering/View/Order/" + orderId, cookievalue, "Order Service Down", _handler);
            return View("Shared/Simple", vm);
        }
    }
}