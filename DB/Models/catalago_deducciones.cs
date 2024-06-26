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

    public partial class catalago_deducciones
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo n�meros")]
        public int idCatalago_Deducciones { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[a-zA-Z�������������� ]*$", ErrorMessage = "El campo debe contener solo letras")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "El campo debe contener solo n�meros")]
        public Nullable<decimal> Monto { get; set; }
    }
}
