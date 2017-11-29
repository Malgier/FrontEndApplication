using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class TokenResponse
    {
        public DateTime RequestAt { get; set; }
        public double ExpiresIn { get; set; }
        public string Type { get; set; }
        public string AccessToken { get; set; }
    }
}
