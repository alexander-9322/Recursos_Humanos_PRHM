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
    
    public partial class calculo_aguinaldo
    {
        public int Id_Aguinaldo { get; set; }
        public int Año { get; set; }
        public int Dias_Trabajados { get; set; }
        public decimal Total_Salarios { get; set; }
        public decimal Aguinaldo { get; set; }
        public int Empleados_Id_Empleado { get; set; }
    }
}
