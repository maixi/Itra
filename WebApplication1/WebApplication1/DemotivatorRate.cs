//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class DemotivatorRate
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public int DemotivatorId { get; set; }
        public string AspNetUserId { get; set; }
    
        public virtual Demotivator Demotivator { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
