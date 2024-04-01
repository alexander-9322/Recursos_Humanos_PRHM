using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.DBConnection;
using DB.Models;
using DB.Models.Usuarios;
using Newtonsoft.Json;

namespace DB.Models.Controllers
{
    public class UsuarioController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            var personas = db.personas
            .Include(p => p.empleados) // Incluir la relación con empleados
            .Include(p => p.direccions) // Incluir la relación con direcciones
            .Include(p => p.telefonoes) // Incluir la relación con teléfonos
            .ToList();

            var viewModelList = new List<Usuario>();

            foreach (var persona in personas)
            {
                var direccion = persona.direccions.FirstOrDefault();
                var telefono = persona.telefonoes.FirstOrDefault();
                var empleado = persona.empleados.FirstOrDefault();
                var usuario = db.usuarios.FirstOrDefault(u => u.Empleados_Id_Empleado == empleado.Id_Empleado); // Obtener el usuario asociado al empleado

                var viewModel = new Usuario
                {
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    DIRECCION = direccion,
                    TELEFONO = telefono,
                    USUARIO = usuario // Agregar el usuario al ViewModel
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }


        public ActionResult InformacionPersonal()
        {
            ViewBag.Distritos = new SelectList(db.distritoes.ToList(), "IdDistrito", "Descripcion");
            ViewBag.Cantones = new SelectList(db.cantons.ToList(), "IdCanton", "Descripcion");
            ViewBag.Provincias = new SelectList(db.provincias.ToList(), "IdProvincia", "Descripcion");

            Usuario user = (Usuario)Session["User"];
            ViewBag.Cedula = user.PERSONA.Cedula;
            ViewBag.IdUsuario = user.USUARIO.IdUsuario;
            ViewBag.Correo = user.USUARIO.Correo;
            ViewBag.Clave = user.USUARIO.Clave;
            ViewBag.Activo = user.USUARIO.Activo;
            ViewBag.Bloqueado = user.USUARIO.Bloqueado;
            ViewBag.Nombre = user.PERSONA.Nombre;
            ViewBag.Apellido1 = user.PERSONA.Apeliido1;
            ViewBag.Apellido2 = user.PERSONA.Apellido2;
            ViewBag.FechaNacimiento = user.PERSONA.Fecha_Nacimiento;
            ViewBag.FechaIngreso = user.EMPLEADO.Fecha_Ingreso;
            ViewBag.Salario = user.EMPLEADO.Salario;
            ViewBag.IdAdministrador = user.EMPLEADO.Jefatura_Id_Empleado;

            return View();
        }
        // GET: Usuario/Details/5
        public ActionResult Details(int cedula)
        {
            if (cedula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DataSet ds = new DataSet();
            Servicios se = new Servicios();

            ds = se.ObtenerInformacion_X_Cedula(cedula);

            Usuario uSUARIO = new Usuario();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                uSUARIO.USUARIO.IdUsuario = (int)dr["Id_Usuario"];
                uSUARIO.USUARIO.Correo = dr["Correo"].ToString();
                uSUARIO.USUARIO.Clave = dr["Clave"].ToString();
                uSUARIO.USUARIO.Activo = (bool)dr["Activo"];
                uSUARIO.USUARIO.Bloqueado = (bool)dr["Bloqueado"];
                uSUARIO.PERSONA.Nombre = dr["Nombre"].ToString();
                uSUARIO.PERSONA.Apeliido1 = dr["Apellido1"].ToString();
                uSUARIO.PERSONA.Apellido2 = dr["Apellido2"].ToString();
                uSUARIO.PERSONA.Fecha_Nacimiento = (DateTime)dr["Fecha_Nacimiento"];
                uSUARIO.EMPLEADO.Fecha_Ingreso = (DateTime)dr["Fecha_Ingreso"];
                uSUARIO.EMPLEADO.Salario = (decimal)dr["Salario"];
                uSUARIO.EMPLEADO.Jefatura_Id_Empleado = (int)dr["Id_Administrador"];
            }

           
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }


        [HttpPost]
        public ActionResult CambiarProvincia(int IdProvincia)
        {
            var cantones = db.cantons.Where(c => c.Provincia_idProvincia == IdProvincia).ToList();
            SelectList cantonesList = new SelectList(cantones, "idCanton", "Descripcion");
            return Json(cantonesList);
        }

        [HttpPost]
        public ActionResult CambiarCanton(int IdCanton)
        {
            var distritos = db.cantons.Where(d => d.Provincia_idProvincia == IdCanton).ToList();
            SelectList distritosList = new SelectList(distritos, "idDistrito", "Descripcion");
            return Json(distritosList);
        }


        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.Catalogo_Cedula = new SelectList(db.catalago_cedula.ToList(), "IdCatalago_Cedula", "Descripcion_Cedula");
            ViewBag.Distritos = new SelectList(db.distritoes.ToList(), "idDistrito", "Descripcion");
            ViewBag.Cantones = new SelectList(db.cantons.ToList(), "idCanton", "Descripcion");
            ViewBag.Provincias = new SelectList(db.provincias.ToList(), "idProvincia", "Descripcion");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    Servicios se = new Servicios();
                    user.USUARIO.Activo = true;
                    user.USUARIO.Bloqueado = false;
                    user.USUARIO.Clave = "Test12345";

                    ds = se.Crear_Persona_Usuario_Empleado(user);
                    string message = ds.Tables[0].Rows[0]["message"].ToString();
                    ViewBag.Message = JsonConvert.SerializeObject(message);
                    return RedirectToAction("Index");
                }

                ViewBag.Catalogo_Cedula = new SelectList(db.catalago_cedula.ToList(), "IdCatalago_Cedula", "Descripcion_Cedula");
                ViewBag.Distritos = new SelectList(db.distritoes.ToList(), "idDistrito", "Descripcion");
                ViewBag.Cantones = new SelectList(db.cantons.ToList(), "idCanton", "Descripcion");
                ViewBag.Provincias = new SelectList(db.provincias.ToList(), "idProvincia", "Descripcion");

                return View(user);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit()
        {
            DataSet ds = new DataSet();
            Servicios se = new Servicios();

            Usuario user = (Usuario)Session["User"];

            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        public ActionResult EditAdmin(long cedula)
        {
            DataSet ds = new DataSet();
            Servicios se = new Servicios();

            ds = se.ObtenerInformacion_X_Cedula(cedula);

            Usuario user = new Usuario();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // Asignar los valores del DATAROW a variables locales
                user.PERSONA.Cedula = (long)dr["Cedula"];
                user.USUARIO.IdUsuario = (int)dr["IdUsuario"];
                user.USUARIO.Correo = dr["Correo"].ToString();
                user.USUARIO.Activo = (bool)dr["Activo"];
                user.USUARIO.Bloqueado = (bool)dr["Bloqueado"];
                user.PERSONA.Nombre = dr["Nombre"].ToString();
                user.PERSONA.Apeliido1 = dr["Apeliido1"].ToString();
                user.PERSONA.Apellido2 = dr["Apellido2"].ToString();
                user.PERSONA.Fecha_Nacimiento = (DateTime)dr["Fecha_Nacimiento"];
                user.EMPLEADO.Fecha_Ingreso = (DateTime)dr["Fecha_Ingreso"];
                user.EMPLEADO.Salario = (decimal)dr["Salario"];
                user.EMPLEADO.Jefatura_Id_Empleado = (int)dr["Jefatura_Id_Empleado"];
                user.EMPLEADO.Id_Empleado = (int)dr["Id_Empleado"];
                user.DIRECCION.Otras_sennas = dr["OtrasSenas"].ToString();
                user.DIRECCION.Distrito_idDistrito = (int)dr["Distrito_idDistrito"];
                user.DIRECCION.Distrito_Canton_idCanton = (int)dr["Distrito_Canton_idCanton"];
                user.DIRECCION.Distrito_Canton_Provincia_idProvincia = (int)dr["Distrito_Canton_Provincia_idProvincia"];
                user.TELEFONO.idTelefono = (int)dr["idTelefono"];
                user.TELEFONO.Activo = (bool)dr["TelefonoActivo"];
            }

            ViewBag.Activo = user.USUARIO.Activo;
            ViewBag.Bloqueado = user.USUARIO.Bloqueado;
            ViewBag.telefonoActivo = user.TELEFONO.Activo;
            ViewBag.Distritos = new SelectList(db.distritoes.ToList(), "idDistrito", "Descripcion", user.DIRECCION.Distrito_idDistrito);
            ViewBag.Cantones = new SelectList(db.cantons.ToList(), "idCanton", "Descripcion", user.DIRECCION.Distrito_Canton_idCanton);
            ViewBag.Provincias = new SelectList(db.provincias.ToList(), "idProvincia", "Descripcion", user.DIRECCION.Distrito_Canton_Provincia_idProvincia);

            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario user)
        {
            if (ModelState.IsValid)
            {
                DataSet ds = new DataSet();
                Servicios se = new Servicios();

                ds = se.Modificar_Datos_Usuario(user);

                return RedirectToAction("LogOut", "Sesiones");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin(Usuario user)
        {


            if (ModelState.IsValid)
            {
                DataSet ds = new DataSet();
                Servicios se = new Servicios();

                ds = se.MODIFICAR_INFORMACION_USUARIO(user);
            }
            ViewBag.Distritos = new SelectList(db.distritoes.ToList(), "idDistrito", "Descripcion", user.DIRECCION.Distrito_idDistrito);
            ViewBag.Cantones = new SelectList(db.cantons.ToList(), "idCanton", "Descripcion", user.DIRECCION.Distrito_Canton_idCanton);
            ViewBag.Provincias = new SelectList(db.provincias.ToList(), "idProvincia", "Descripcion", user.DIRECCION.Distrito_Canton_Provincia_idProvincia);
            return RedirectToAction("Index");
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario uSUARIO = db.usuarios.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario uSUARIO = db.usuarios.Find(id);
            db.usuarios.Remove(uSUARIO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        
    }
}
