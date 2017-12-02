using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontEndApp.Models;
using System.Net.Http;

namespace FrontEndApp.Controllers
{
    [Route("[controller]")]
    public class MessageController : Controller
    {
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
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient("http://localhost:50143", "/MessagesMVC/MyMessages/" + id, cookievalue, "Messaging Service Down");
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
            if (Request.Cookies["token"] != null)
            {
                cookievalue = Request.Cookies["token"].ToString();
            }

            PartialVM vm = client.GetClient("http://localhost:50143", "/MessagesMVC/send/" + id, cookievalue, "Messaging Service Down");
            return View(vm);
        }

        [HttpPost]
        [Route("SaveMessage")]
        public IActionResult SaveMessage(MessageVM model)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync("http://localhost:50143/MessagesMVC/SaveMessage", model).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var conent = response.Content.ReadAsAsync()
                }
                return Ok();
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

            PartialVM vm = client.GetClient("http://localhost:50143", "/MessagesMVC/details/" + id, cookievalue, "");
            return View(vm);
        }
    }
}