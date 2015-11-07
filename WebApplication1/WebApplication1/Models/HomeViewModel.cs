using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomeViewModel
    {
        public ICollection<Demotivator> demotivators { get; set; }
        public ICollection<tag> tags { get; set; }
        public int DemCount { get; set; }       
    }

}