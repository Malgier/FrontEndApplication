using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
