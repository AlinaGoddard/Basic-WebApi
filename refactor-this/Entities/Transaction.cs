//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public System.Guid Id { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
