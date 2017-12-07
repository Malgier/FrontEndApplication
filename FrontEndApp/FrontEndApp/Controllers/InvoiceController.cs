using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using FrontEndApp.Models;

namespace FrontEndApp.Controllers
{
    public class InvoiceController : Controller
    {
        HttpMessageHandler _handler;
        private string invoiceServiceLink;

        public InvoiceController(IConfiguration config, HttpMessageHandler handler = null)
        {
            _handler = handler == null ? new HttpClientHandler() : handler;
            invoiceServiceLink = config.GetValue<string>("InvoiceService");
        }

        public IActionResult InvoicesForApproval()
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(invoiceServiceLink, "/api/Invoice/Views/InvoicesForApproval", cookievalue, "Invoice Service Down", _handler);
            return View(vm);
        }


        public IActionResult InvoicesForUser(string id)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(invoiceServiceLink, "/api/Invoice/Views/InvoicesForUsers/" + id, cookievalue, "Invoice Service Down", _handler);
            return View(vm);
        }


        public IActionResult InvoicesDetails(string reference)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(invoiceServiceLink, "/api/Invoice/Views/InvoiceDetails/" + reference, cookievalue, "Invoice Service Down", _handler);
            return View(vm);
        }
    }
}