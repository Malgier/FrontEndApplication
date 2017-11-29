using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class MessageVM
    {
        public int MessageID { get; set; }
        public String Title { get; set; }
        public String MessageContext { get; set; }
        public DateTime DateSent { get; set; }
        public int SenderUserID { get; set; }
        public int ReceiverUserID { get; set; }
    }
}
