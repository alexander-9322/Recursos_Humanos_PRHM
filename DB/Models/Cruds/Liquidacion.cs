using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Cruds
{
    public class Liquidacion
    {
        public catalago_tipo_liquidacion TIPO_LIQUDACION { get; set; }
        public empleado EMPLEADO { get; set; }
        public persona PERSONA { get; set; }
        public detalles_liquidacion DETALLES_LIQUIDACION { get; set; }
        public liquidacion LIQUIDACION { get; set; }

        public int HizoPreaviso { get; set; }

        public Liquidacion()
        {
            TIPO_LIQUDACION = new catalago_tipo_liquidacion();
            EMPLEADO = new empleado();
            PERSONA = new persona();
            DETALLES_LIQUIDACION = new detalles_liquidacion();
            LIQUIDACION = new liquidacion();
        }
    }
}
