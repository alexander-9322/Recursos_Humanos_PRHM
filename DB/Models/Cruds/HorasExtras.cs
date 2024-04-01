using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Cruds
{
    public class HorasExtras
    {
        public horas_extras HORAS_EXTRAS { get; set; }
        public persona PERSONA { get; set; }
        public empleado EMPLEADO { get; set; }
        public catalago_tipo_horas_extras TIPO_HORAS_EXTRA { get; set; }


        public HorasExtras()
        {
            HORAS_EXTRAS = new horas_extras();
            PERSONA = new persona();
            EMPLEADO = new empleado();
            TIPO_HORAS_EXTRA = new catalago_tipo_horas_extras();
        }
    }
}
