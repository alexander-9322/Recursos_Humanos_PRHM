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
    
    public partial class detalles_planilla
    {
        public int idDetalles_Planilla { get; set; }
        public float Total_HorasExtra { get; set; }
        public float Total_Incapacidades { get; set; }
        public float Total_Deducciones { get; set; }
        public float SubTotal { get; set; }
        public int Planilla_Id_Planilla { get; set; }
        public int Planilla_Empleados_Id_Empleado { get; set; }
    
        public virtual planilla planilla { get; set; }
    }
}