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
    using System.ComponentModel.DataAnnotations;

    public partial class telefono
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[245678]\\d*$", ErrorMessage = "El campo debe iniciar con los n�meros 2, 4, 5, 6, 7 u 8")]
        [Range(10000000, 99999999, ErrorMessage = "El numero debe de contener un minimo de 8 digitos")]
        public int idTelefono { get; set; }
        public bool Activo { get; set; }
        public long Persona_Cedula { get; set; }
    
        public virtual persona persona { get; set; }
    }
}