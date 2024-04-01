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

    public partial class control_de_permiso_colaborador
    {
        public int Id_Permiso { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public System.DateTime Fecha_Solicitud { get; set; }
        public int Empleados_Id_Empleado { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo n�meros")]
        public int IdTipoPermiso { get; set; }
        public bool Abrobado { get; set; }
        public virtual empleado empleado { get; set; }
        public virtual catalogo_control_permisos_colaborador catalogo_control_permisos_colaborador { get; set; }
    }
}