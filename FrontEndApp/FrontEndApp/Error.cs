using FrontEndApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp
{
    public class Error
    {
        public PartialVM ErrorCheck(string errorMessage)
        {
            PartialVM vm = new PartialVM()
            {
                PartialView = "<h2>" + errorMessage + "</h2>"
            };
            return vm;
        }
    }
}
