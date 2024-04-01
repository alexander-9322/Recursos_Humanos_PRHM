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

    public partial class vacacione
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public System.DateTime Fecha_Inicio { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public System.DateTime Fecha_Fin { get; set; }

        [Required(ErrorMessage = "El campo Cantidad de D�as es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La Cantidad de D�as debe ser mayor que cero.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo n�meros")]
        public int Cantidad_Dias { get; set; }
        public int Aprobaciones_IdAprobaciones { get; set; }
        public int Empleados_Id_Empleado { get; set; }
    }
}
