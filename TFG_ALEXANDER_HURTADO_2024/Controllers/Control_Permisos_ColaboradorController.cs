using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.DBConnection;
using DB.Models;

namespace DB.Models.Controllers
{
    public class Control_Permisos_ColaboradorController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Control_Permisos_Colaborador
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            Servicios se = new Servicios();
            ds = se.Get_Info_Permisos();

            List<Models.control_de_permiso_colaborador> permisos = new List<Models.control_de_permiso_colaborador>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                control_de_permiso_colaborador permiso = new control_de_permiso_colaborador();

                permiso.Id_Permiso = Convert.ToInt32(row["Id_Permiso"]);
                permiso.IdTipoPermiso = Convert.ToInt32(row["IdCatalogo_Control_permisos_Colaborador"]);
                permiso.Empleados_Id_Empleado = Convert.ToInt32(row["Empleados_Id_Empleado"]);
                permiso.Fecha_Solicitud = Convert.ToDateTime(row["Fecha_Solicitud"]);
                permiso.Abrobado = Convert.ToBoolean(row["Abrobado"]);
                // Crear e inicializar EMPLEADO
                permiso.empleado = new Models.empleado();

                // Crear e inicializar PERSONA
                permiso.empleado.persona = new Models.persona();
                permiso.empleado.persona.Nombre = row["Nombre"].ToString() + " " + row["Apeliido1"].ToString() + " " + row["Apellido2"].ToString();

                // Crear e inicializar Catalogo_Control_permisos_Colaborador si es necesario
                permiso.catalogo_control_permisos_colaborador = new Models.catalogo_control_permisos_colaborador();
                permiso.catalogo_control_permisos_colaborador.Descripcion = row["Descripcion"].ToString();

                permisos.Add(permiso);
            }

            return View(permisos);
        }



        public ActionResult PermisosPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];
            var cONTROL_DE_PERMISO_COLABORADOR = db.control_de_permiso_colaborador.Include(c => c.catalogo_control_permisos_colaborador).Where(p => p.Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);
            return View(cONTROL_DE_PERMISO_COLABORADOR.ToList());
        }

        // GET: Control_Permisos_Colaborador/Details/5
        public ActionResult Details(DateTime Fecha_Aprobacion, int? idEmpleado, int? idCatalogoPermiso)
        {
            if (Fecha_Aprobacion == null || idEmpleado == null || idCatalogoPermiso == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.control_de_permiso_colaborador cONTROL_DE_PERMISO_COLABORADOR = db.control_de_permiso_colaborador.FirstOrDefault(cdp => cdp.Fecha_Solicitud == Fecha_Aprobacion && cdp.Empleados_Id_Empleado == idEmpleado && cdp.IdTipoPermiso == idCatalogoPermiso);
            if (cONTROL_DE_PERMISO_COLABORADOR == null)
            {
                return HttpNotFound();
            }
            return View(cONTROL_DE_PERMISO_COLABORADOR);
        }

        // GET: Control_Permisos_Colaborador/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoPermiso = new SelectList(db.catalogo_control_permisos_colaborador, "IdCatalogo_Control_permisos_Colaborador", "Descripcion");
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre");
            return View();
        }

        // POST: Control_Permisos_Colaborador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(control_de_permiso_colaborador cONTROL_DE_PERMISO_COLABORADOR)
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];

            cONTROL_DE_PERMISO_COLABORADOR.Empleados_Id_Empleado = user.EMPLEADO.Id_Empleado;

            cONTROL_DE_PERMISO_COLABORADOR.Abrobado = false;

            DataSet ds = new DataSet();
            Servicios servicios = new Servicios();
            ds = servicios.SP_Obtener_IdPermisoColaborador();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cONTROL_DE_PERMISO_COLABORADOR.Id_Permiso = Convert.ToInt32(row["IdPermiso"]);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    db.control_de_permiso_colaborador.Add(cONTROL_DE_PERMISO_COLABORADOR);
                    db.SaveChanges();
                    return RedirectToAction("PermisosPersonal");
                }

                ViewBag.IdTipoPermiso = new SelectList(db.catalogo_control_permisos_colaborador, "IdCatalogo_Control_permisos_Colaborador", "Descripcion");
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre");
                return View(cONTROL_DE_PERMISO_COLABORADOR);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("PermisosPersonal");
            }
            
        }

        // GET: Control_Permisos_Colaborador/Edit/5
        public ActionResult Edit(DateTime Fecha_Aprobacion, int? idEmpleado, int? idCatalogoPermiso)
        {
            if (Fecha_Aprobacion == null || idEmpleado == null || idCatalogoPermiso == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.control_de_permiso_colaborador cONTROL_DE_PERMISO_COLABORADOR = db.control_de_permiso_colaborador.FirstOrDefault(cdp => cdp.Fecha_Solicitud == Fecha_Aprobacion && cdp.Empleados_Id_Empleado == idEmpleado && cdp.IdTipoPermiso == idCatalogoPermiso);
            if (cONTROL_DE_PERMISO_COLABORADOR == null)
            {
                return HttpNotFound();
            }

            cONTROL_DE_PERMISO_COLABORADOR.Abrobado = true;

            db.Entry(cONTROL_DE_PERMISO_COLABORADOR).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Control_Permisos_Colaborador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DB.Models.control_de_permiso_colaborador cONTROL_DE_PERMISO_COLABORADOR)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cONTROL_DE_PERMISO_COLABORADOR).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IdTipoPermiso = new SelectList(db.catalogo_control_permisos_colaborador, "IdCatalogo_Control_permisos_Colaborador", "Descripcion", cONTROL_DE_PERMISO_COLABORADOR.IdTipoPermiso);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", cONTROL_DE_PERMISO_COLABORADOR.Empleados_Id_Empleado);
                return View(cONTROL_DE_PERMISO_COLABORADOR);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Control_Permisos_Colaborador/Delete/5
        public ActionResult Delete(DateTime Fecha_Aprobacion, int? idEmpleado, int? idCatalogoPermiso)
        {
            if (Fecha_Aprobacion == null || idEmpleado == null || idCatalogoPermiso == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.control_de_permiso_colaborador cONTROL_DE_PERMISO_COLABORADOR = db.control_de_permiso_colaborador.FirstOrDefault(cdp => cdp.Fecha_Solicitud == Fecha_Aprobacion && cdp.Empleados_Id_Empleado == idEmpleado && cdp.IdTipoPermiso == idCatalogoPermiso);
            if (cONTROL_DE_PERMISO_COLABORADOR == null)
                if (cONTROL_DE_PERMISO_COLABORADOR == null)
            {
                return HttpNotFound();
            }
            return View(cONTROL_DE_PERMISO_COLABORADOR);
        }

        // POST: Control_Permisos_Colaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime Fecha_Aprobacion, int? idEmpleado, int? idCatalogoPermiso)
        {
            try
            {
                control_de_permiso_colaborador cONTROL_DE_PERMISO_COLABORADOR = db.control_de_permiso_colaborador.FirstOrDefault(cdp => cdp.Fecha_Solicitud == Fecha_Aprobacion && cdp.Empleados_Id_Empleado == idEmpleado && cdp.IdTipoPermiso == idCatalogoPermiso);
                db.control_de_permiso_colaborador.Remove(cONTROL_DE_PERMISO_COLABORADOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                Session["Error"] = "No se pudo eliminar";
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
