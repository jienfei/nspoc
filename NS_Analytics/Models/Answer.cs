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
    
    public partial class Answer
    {
        public int Id { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> PeriodId { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual Period Period { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}