using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Sesion
{
    public partial class Login
    {
        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Correo no es una dirección de correo electrónico válida.")]
        public string correo { get; set; }

        [Required(ErrorMessage = "El campo Clave es obligatorio.")]
        [DataType(DataType.Password)]
        public string clave { get; set; }


        public bool ValidarUsuario(bool bloqueado, bool activo)
        {
            bool valido = false;

            if (bloqueado == false && activo == true)
            {
                valido = true;
            }

            return valido;
        }
    }
}
