//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class distrito
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public distrito()
        {
            this.direccions = new HashSet<direccion>();
        }
    
        public int idDistrito { get; set; }
        public string Descripcion { get; set; }
        public int Canton_idCanton { get; set; }
        public int Canton_Provincia_idProvincia { get; set; }
    
        public virtual canton canton { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<direccion> direccions { get; set; }
    }
}
