using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEndApp.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private string messageServiceLink;

        public MessageController(IConfiguration config)
        {
            messageServiceLink = config.GetValue<string>("MessageService");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("MyMessages/{id}")]
        public IActionResult MyMessages(int id)
        {
            Client client = new Client();
            
            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(messageServiceLink, "/MessagesMVC/MyMessages/" + id, cookievalue, "Messaging Service Down");
            return View(vm);
        }

        [HttpGet]
        [Route("Send/{id}")]
        // GET: Messages/Send/1
        public IActionResult Send(int id)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["access_token"] != null)
            {
                cookievalue = Request.Cookies["access_token"].ToString();
            }

            PartialVM vm = client.GetClient(messageServiceLink, "/MessagesMVC/send/" + id, cookievalue, "Messaging Service Down");
            return View(vm);
        }

        [HttpPost]
        [Route("SaveMessage")]
        public IActionResult SaveMessage(MessageVM model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(messageServiceLink + "/MessagesMVC/SaveMessage", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [Route("Details/{id}")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            Client client = new Client();

            //Read cookie
            string cookievalue = "";
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient(messageServiceLink, "/MessagesMVC/details/" + id, cookievalue, "");
            return View(vm);
        }
    }
}