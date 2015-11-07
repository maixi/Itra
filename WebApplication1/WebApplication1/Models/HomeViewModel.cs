using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomeViewModel
    {
        public ICollection<Demotivator> newDemotivators { get; set; }
        public ICollection<Demotivator> bestDemotivators { get; set; }
        public ICollection<tag> tags { get; set; }
        public int DemCount { get; set; }       
    }

}