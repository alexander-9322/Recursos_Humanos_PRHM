using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Cruds
{
    public class Incapacidades
    {
        public incapacidade INCAPACIDADES { get; set; }
        public persona PERSONA { get; set; }
        public empleado EMPLEADO { get; set; }
        public catalogo_tipo_incapacidades catalogo_Tipo_Incapacidades { get; set; }


        public Incapacidades()
        {
            INCAPACIDADES = new incapacidade();
            PERSONA = new persona();
            EMPLEADO = new empleado();
            catalogo_Tipo_Incapacidades = new catalogo_tipo_incapacidades();
        }
    }
}
