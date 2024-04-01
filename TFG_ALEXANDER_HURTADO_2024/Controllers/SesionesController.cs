using Antlr.Runtime.Misc;
using DB.DBConnection;
using DB.Models;
using DB.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DB.Models;

namespace DB.Models.Controllers
{
    public class SesionesController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidarCredenciales(DB.Models.Sesion.Login login)
        {
            if (!ModelState.IsValid)
            {
                Session["Error"] = "Usuario o contraseña incorrectas";
                return RedirectToAction("Login");
            }

            DataSet ds = new DataSet();
            Servicios se = new Servicios();
            Usuario user = new Usuario();


            ds = se.Login(login);

            if (ds.Tables[0].Columns.Contains("message") && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["message"] != DBNull.Value)
            {
                Session["Error"] = "Usuario o contraseña incorrectas";
                return RedirectToAction("Login");
            }


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // Asignar los valores del DATAROW a variables locales
                user.USUARIO.IdUsuario = (int)dr["IdUsuario"];
                user.USUARIO.Correo = dr["Correo"].ToString();
                user.USUARIO.Clave = dr["Clave"].ToString();
                user.USUARIO.Activo = (bool)dr["Activo"];
                user.USUARIO.Bloqueado = (bool)dr["Bloqueado"];
                user.PERSONA.Nombre = dr["Nombre"].ToString();
                user.PERSONA.Apeliido1 = dr["Apeliido1"].ToString();
                user.PERSONA.Apellido2 = dr["Apellido2"].ToString();
                user.PERSONA.Fecha_Nacimiento = (DateTime)dr["Fecha_Nacimiento"];
                user.EMPLEADO.Fecha_Ingreso = (DateTime)dr["Fecha_Ingreso"];
                user.EMPLEADO.Salario = (decimal)dr["Salario"];
                user.EMPLEADO.Jefatura_Id_Empleado = (int)dr["Jefatura_Id_Empleado"];
                user.PERSONA.Cedula = (long)dr["Persona_Cedula"];
                user.EMPLEADO.Persona_Cedula = (long)dr["Persona_Cedula"];
                user.EMPLEADO.Id_Empleado = (int)dr["Id_Empleado"];
                user.USUARIO.PrimerIngreso = (bool)dr["PrimerIngreso"];
                Session["User"] = user;
            }

            if (!login.ValidarUsuario((bool)user.USUARIO.Bloqueado, (bool)user.USUARIO.Activo))
            {
                Session["Error"] = "Usuario inactivo o bloqueado";
                return RedirectToAction("Login");
            }

            if ((bool)user.USUARIO.PrimerIngreso)
            {
                return RedirectToAction("Edit", "Usuario");
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult LogOut()
        {
            Session["User"] = null;

            return RedirectToAction("Login");
        }

    }
}
