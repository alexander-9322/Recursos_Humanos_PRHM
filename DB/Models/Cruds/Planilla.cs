using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Cruds
{
    public class Planilla
    {
        public detalles_planilla DETALLES_PLANILLA { get; set; }
        public planilla PLANILLA { get; set; }
        public catalago_deducciones DEDUCCIONES { get; set; }
        public persona PERSONA { get; set; }
        public empleado EMPLEADO { get; set; }

        public Planilla()
        {
            this.DETALLES_PLANILLA = new detalles_planilla();
            this.PLANILLA = new planilla();
            this.DEDUCCIONES = new catalago_deducciones();
            this.PERSONA = new persona();
            this.EMPLEADO = new empleado(); 
        }
    }
}
