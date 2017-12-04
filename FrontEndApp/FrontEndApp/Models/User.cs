using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class User
    {
        public virtual int ID { get; set; }
        public virtual string UserID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual bool Authorised { get; set; }
        public virtual bool Active { get; set; }
    }
}
