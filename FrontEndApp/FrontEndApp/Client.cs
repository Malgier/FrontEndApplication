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
        /// <summary>
        ///
        /// </summary>
        /// <param name="baseLink">Base URL</param>
        /// <param name="path">Sub path of URL</param>
        /// <param name="token">token being passed by services</param>
        /// <param name="errorMessage">error message to display if current service is down</param>
        /// <param name="handler">handler used only for testing purposes</param>
        /// <returns></returns>
        public PartialVM GetClient(string baseLink, string path, string token, string errorMessage, HttpMessageHandler handler)
        {
            using (HttpClient client = new HttpClient(handler, false))
            {
                try
                {
                    client.BaseAddress = new Uri(baseLink);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                    if(token != "")
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

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
                        return new PartialVM()
                        {
                            PartialView = "<h2>" + errorMessage + "</h2>"
                        };
                    }
                }
                catch (Exception e)
                {
                    return new PartialVM()
                    {
                        PartialView = "<h2> Error </h2> <div>" + errorMessage + "</div> <div>" + e.Message + "</div>"
                    };
                }
            }
        }
    }
}
