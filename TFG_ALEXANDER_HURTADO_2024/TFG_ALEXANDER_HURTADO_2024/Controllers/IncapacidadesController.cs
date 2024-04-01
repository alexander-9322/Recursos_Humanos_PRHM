using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.Models;
using DB.Models.Cruds;
using DB.Models.Usuarios;

namespace TFG_ALEXANDER_HURTADO_2024.Controllers
{
    public class IncapacidadesController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Incapacidades
        public ActionResult Index()
        {

            var incapacidades = db.incapacidades
            .Include(i => i.catalogo_tipo_incapacidades)
            .ToList();

            var viewModelList = new List<Incapacidades>();

            foreach (var incapacidad in incapacidades)
            {
                var tipoIncapacidad = db.catalogo_tipo_incapacidades.FirstOrDefault(u => u.idCatalogo_Tipo_Incapacidades == incapacidad.IdTipoIncapacidad);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == incapacidad.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Incapacidades
                {
                    INCAPACIDADES = incapacidad,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    catalogo_Tipo_Incapacidades = tipoIncapacidad
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        public ActionResult IncapacidadPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];
            var iNCAPACIDADES = db.incapacidades.Include(i => i.catalogo_tipo_incapacidades).Where(i => i.Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);
            return View(iNCAPACIDADES.ToList());
        }

        // GET: Incapacidades/Details/5
        public ActionResult Details(int? id, int? idTipoIncapacidad, int? idEmpleado)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            incapacidade iNCAPACIDADE = db.incapacidades.FirstOrDefault(i => i.idIncapacidades == id && i.IdTipoIncapacidad == idTipoIncapacidad && i.Empleados_Id_Empleado == idEmpleado);
            if (iNCAPACIDADE == null)
            {
                return HttpNotFound();
            }
            return View(iNCAPACIDADE);
        }

        // GET: Incapacidades/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoIncapacidad = new SelectList(db.catalogo_tipo_incapacidades, "idCatalogo_Tipo_Incapacidades", "Descripcion");
            // Suponiendo que db es tu contexto de base de datos
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre");
            return View();
        }

        // POST: Incapacidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(incapacidade iNCAPACIDADE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.incapacidades.Add(iNCAPACIDADE);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdTipoIncapacidad = new SelectList(db.catalogo_tipo_incapacidades, "idCatalogo_Tipo_Incapacidades", "Descripcion", iNCAPACIDADE.IdTipoIncapacidad);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", iNCAPACIDADE.Empleados_Id_Empleado);
                return View(iNCAPACIDADE);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Incapacidades/Edit/5
        public ActionResult Edit(int? id, int? idTipoIncapacidad, int? idEmpleado)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            incapacidade iNCAPACIDADE = db.incapacidades.FirstOrDefault(i => i.idIncapacidades == id && i.IdTipoIncapacidad == idTipoIncapacidad && i.Empleados_Id_Empleado == idEmpleado);
            if (iNCAPACIDADE == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoIncapacidad = new SelectList(db.catalogo_tipo_incapacidades, "idCatalogo_Tipo_Incapacidades", "Descripcion", iNCAPACIDADE.catalogo_tipo_incapacidades);
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", iNCAPACIDADE.Empleados_Id_Empleado);
            return View(iNCAPACIDADE);
        }

        // POST: Incapacidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(incapacidade iNCAPACIDADE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(iNCAPACIDADE).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IdTipoIncapacidad = new SelectList(db.catalogo_tipo_incapacidades, "idCatalogo_Tipo_Incapacidades", "Descripcion", iNCAPACIDADE.IdTipoIncapacidad);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", iNCAPACIDADE.Empleados_Id_Empleado);
                return View(iNCAPACIDADE);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Incapacidades/Delete/5
        public ActionResult Delete(int? id, int? idTipoIncapacidad, int? idEmpleado)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            incapacidade iNCAPACIDADE = db.incapacidades.FirstOrDefault(i => i.idIncapacidades == id && i.IdTipoIncapacidad == idTipoIncapacidad && i.Empleados_Id_Empleado == idEmpleado);
            if (iNCAPACIDADE == null)
            {
                return HttpNotFound();
            }
            return View(iNCAPACIDADE);
        }

        // POST: Incapacidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? idTipoIncapacidad, int? idEmpleado)
        {
            try
            {
                incapacidade iNCAPACIDADE = db.incapacidades.FirstOrDefault(i => i.idIncapacidades == id && i.IdTipoIncapacidad == idTipoIncapacidad && i.Empleados_Id_Empleado == idEmpleado);
                db.incapacidades.Remove(iNCAPACIDADE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
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
