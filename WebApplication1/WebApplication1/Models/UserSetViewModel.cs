using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class UserSetViewModel 
    {
        // GET: UserSetViewModel
        public ICollection<Demotivator> Demotivators { get; set; }
        public int DemCount { get; set; }

    }
}