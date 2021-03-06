//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NS_Analytics.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Period
    {
        public Period()
        {
            this.Answer = new HashSet<Answer>();
            this.UserPeriod = new HashSet<UserPeriod>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int ProjectId { get; set; }
    
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<UserPeriod> UserPeriod { get; set; }
    }
}
