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
    
    public partial class aprobacione
    {
        public int IdAprobaciones { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }
        public System.DateTime Fecha_respuesta { get; set; }
        public int Empleados_Id_Empleado { get; set; }
    }
}
