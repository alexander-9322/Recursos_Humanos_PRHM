using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Pagos
{
    public class PagoPlanilla
    {
        public planilla PLANILLA { get; set; }
        public empleado EMPLEADO { get; set; }
        public persona PERSONA { get; set; }

        public PagoPlanilla()
        {
            PLANILLA = new planilla();
            EMPLEADO = new empleado();
            PERSONA = new persona();
        }
    }
}
