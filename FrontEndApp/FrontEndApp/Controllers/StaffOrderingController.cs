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
    public class StaffOrderingController : Controller
    {
        HttpMessageHandler _handler;
        private string staffOrderingServiceLink;
        private string productServiceLink;

        public StaffOrderingController(IConfiguration config, HttpMessageHandler handler = null)
        {
            _handler = handler == null ? new HttpClientHandler() : handler;
            staffOrderingServiceLink = config.GetValue<string>("StaffService");
            productServiceLink = config.GetValue<string>("ProductService");
        }

        [Route("StockOrdering/Index")]
        public IActionResult Index()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/Products", cookievalue, "Stock Ordering Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("StockOrdering/Cart")]
        public IActionResult Cart()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/Cart", cookievalue, "Stock Ordering Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("StockOrdering/LowStock")]
        public IActionResult LowStock()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/LowStock", cookievalue, "Stock Ordering Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("StockOrdering/Orders")]
        public IActionResult Orders()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/Orders", cookievalue, "Stock Ordering Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("StockOrdering/Order/{orderId}")]
        public IActionResult Order(int orderId)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/Products/Order/" + orderId, cookievalue, "Stock Ordering Service Down", _handler);
            return View("Shared/Simple", vm);
        }

        [Route("StockOrdering/ProductDetails")]
        public IActionResult ProductDetails(string ean)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            List<string> vms = new List<string>();

            vms.Add(client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/ProductDetails?ean=" + ean, cookievalue, "Stock Ordering Service Down", _handler).PartialView);
            vms.Add(client.GetClient(staffOrderingServiceLink, "api/Product/Views/PriceHistory?ean=" + ean, cookievalue, "Product Service Down", _handler).PartialView);

            return View("Shared/Partials", vms);
        }

        [Route("StockOrdering/OrderCart")]
        public IActionResult OrderCart()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            string vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/OrderCart", cookievalue, "Stock Ordering Service Down", _handler).PartialView;

            return View("Shared/Simple", vm);
        }

        [Route("StockOrdering/ResumeOrder/{orderId}")]
        public IActionResult ResumeOrder(int orderId)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            string vm = client.GetClient(staffOrderingServiceLink, "api/StaffOrdering/View/ResumeOrder", cookievalue, "Stock Ordering Service Down", _handler).PartialView;

            return View("Shared/Simple", vm);
        }
    }
}