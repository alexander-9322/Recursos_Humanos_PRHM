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
    
    public partial class evaluacion_remndimiento_colaborador
    {
        public int Id_Evaluacion { get; set; }
        public System.DateTime Fecha_Evaluacion { get; set; }
        public decimal Calificacion { get; set; }
        public string Retroalimentacion { get; set; }
        public int Catalago_Desempeño_Id_Desempeño { get; set; }
        public int Empleados_Id_Empleado { get; set; }
        public virtual empleado empleado { get; set; }
        public virtual catalago_desempeño catalago_desempeño { get; set; }
    }
}