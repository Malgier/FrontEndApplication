using FrontEndApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp
{
    public class Client
    {
        public PartialVM GetClient(string baseLink, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseLink);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

                HttpResponseMessage response = client.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                {
                    PartialVM vm = new PartialVM();
                    vm.PartialView = response.Content.ReadAsStringAsync().Result;
                    response.Dispose();

                    return vm;
                }
                else
                {
                    return new PartialVM();
                }
            }
        }
    }
}
