using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Cruds
{
    public class Aguinaldo
    {
        public calculo_aguinaldo AGUINALDO { get; set; }
        public persona PERSONA { get; set; }
        public empleado EMPLEADO { get; set; }


        public Aguinaldo()
        {
            AGUINALDO = new calculo_aguinaldo();
            PERSONA = new persona();
            EMPLEADO = new empleado();
        }
    }
}
