//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CityMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CityMaster()
        {
            this.Contacts = new HashSet<Contacts>();
        }
    
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual StateMaster StateMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contacts> Contacts { get; set; }
    }
}
