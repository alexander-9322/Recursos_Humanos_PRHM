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
    
    public partial class horas_extras
    {
        public int Id_Horas { get; set; }
        public int Cantidad_Horas_Extras { get; set; }
        public System.DateTime Fecha_Horas_Trabajadas { get; set; }
        public int Empleados_Id_Empleado { get; set; }
        public int Catalago_Tipo_Horas_Extras_Id_Hora_Extra { get; set; }
        public float Monto { get; set; }
        public virtual empleado empleado { get; set; }
        public virtual catalago_tipo_horas_extras catalago_tipo_horas_extras { get; set; }
    }
}
