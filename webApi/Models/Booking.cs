//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Tourists = new HashSet<Tourist>();
        }
    
        public int Id { get; set; }
        public string Status { get; set; }
        public int PId { get; set; }
        public int SafariId { get; set; }
        public int GateId { get; set; }
        public int VehicleId { get; set; }
        public decimal TotalCost { get; set; }
    
        public virtual Gate Gate { get; set; }
        public virtual Park Park { get; set; }
        public virtual SafariDetail SafariDetail { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tourist> Tourists { get; set; }
    }
}
