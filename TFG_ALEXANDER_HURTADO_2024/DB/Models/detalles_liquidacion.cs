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
    
    public partial class detalles_liquidacion
    {
        public int Id_Detalle_Liquidacion { get; set; }
        public float Monto_Preaviso { get; set; }
        public float Monto_Censantia { get; set; }
        public float Monto_Vacaciones { get; set; }
        public float Monto_Aguinaldo { get; set; }
        public float Total_Liquidacion { get; set; }
        public int Liquidacion_Id_Liquidacion { get; set; }
        public int Liquidacion_Empleados_Id_Empleado { get; set; }
    
        public virtual liquidacion liquidacion { get; set; }
    }
}
