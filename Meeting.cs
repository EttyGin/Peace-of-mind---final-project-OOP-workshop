//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace loginDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meeting
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public System.DateTime Date { get; set; }
        public string Summary { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
    }
}