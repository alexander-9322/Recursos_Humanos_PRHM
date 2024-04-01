using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Cruds
{
    public class Vacaciones
    {
        public vacacione VACACION { get; set; }
        public empleado EMPLEADO { get; set; }
        public persona PERSONA { get; set; }
        public aprobacione APROBACION { get; set; }

        public Vacaciones() 
        {
            VACACION = new vacacione();
            EMPLEADO = new empleado();
            PERSONA = new persona();
            APROBACION = new aprobacione();
        }
    }
}
