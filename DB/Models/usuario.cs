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

    public partial class usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Coloca un correo valido")]
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string ClaveConfirmacion { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public int Empleados_Id_Empleado { get; set; }
        public bool PrimerIngreso { get; set; }
    }
}