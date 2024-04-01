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
    using System.ComponentModel.DataAnnotations;

    public partial class evaluacion_remndimiento_colaborador
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        public int Id_Evaluacion { get; set; }

        public System.DateTime Fecha_Evaluacion { get; set; }

        public decimal Calificacion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ ]*$", ErrorMessage = "El campo debe contener solo letras")]
        public string Retroalimentacion { get; set; }
        public int Catalago_Desempeño_Id_Desempeño { get; set; }
        public int Empleados_Id_Empleado { get; set; }
        public virtual empleado empleado { get; set; }
        public virtual catalago_desempeño catalago_desempeño { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta1 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]

        public int Pregunta2 { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta3 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta4 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta5 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta6 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta7 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta8 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta9 { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe contener solo números")]
        [Range(1, 5, ErrorMessage = "El campo no puede ser menor de 1 y mayor a 5")]
        public int Pregunta10 { get; set; }



    }
}