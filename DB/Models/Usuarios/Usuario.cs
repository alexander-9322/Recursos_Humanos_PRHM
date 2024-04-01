using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Usuarios
{
    public class Usuario
    {
        public persona PERSONA { get; set; }
        public empleado EMPLEADO { get; set; }
        public usuario USUARIO { get; set; }
        public direccion DIRECCION { get; set; }
        public telefono TELEFONO { get; set; }

        public Usuario()
        {
            PERSONA = new persona();
            EMPLEADO = new empleado();
            USUARIO = new usuario();
            DIRECCION = new direccion();
            TELEFONO = new telefono();
        }
    }
}
