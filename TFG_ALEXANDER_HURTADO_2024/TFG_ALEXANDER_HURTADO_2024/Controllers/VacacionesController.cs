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
using DB.Models.Cruds;
using DB.Models.Usuarios;

namespace TFG_ALEXANDER_HURTADO_2024.Controllers
{
    public class VacacionesController : Controller
    {
        private RHEntities db = new RHEntities();


        // GET: Vacaciones
        public ActionResult Index()
        {
            var vacaciones = db.vacaciones;

            var viewModelList = new List<Vacaciones>();

            foreach (var vacacion in vacaciones)
            {
                var aprobacion = db.aprobaciones.FirstOrDefault(u => u.IdAprobaciones == vacacion.Aprobaciones_IdAprobaciones);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == vacacion.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Vacaciones
                {
                    VACACION = vacacion,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    APROBACION = aprobacion
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        public ActionResult VacacionesPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];
            DataSet ds = new DataSet();
            Servicios se = new Servicios();

            ds = se.Validar_Dias_Vacaciones(user.EMPLEADO.Persona_Cedula);
            int.TryParse(ds.Tables[0].Rows[0]["DiasVacaciones"].ToString(), out int cantidadDias);

            ViewBag.cantidad_Dias = cantidadDias;

            var vacaciones = db.vacaciones.Where(v => v.Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);

            var viewModelList = new List<Vacaciones>();

            foreach (var vacacion in vacaciones)
            {
                var aprobacion = db.aprobaciones.FirstOrDefault(u => u.IdAprobaciones == vacacion.Aprobaciones_IdAprobaciones);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == vacacion.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Vacaciones
                {
                    VACACION = vacacion,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    APROBACION = aprobacion
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }


        // GET: Vacaciones/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacacione vACACIONE = db.vacaciones.Find(id);
            if (vACACIONE == null)
            {
                return HttpNotFound();
            }
            return View(vACACIONE);
        }

        // GET: Vacaciones/Create
        public ActionResult Create()
        {
            ViewBag.IdAprobaciones = new SelectList(db.aprobaciones, "IdAprobaciones", "Estado");
            ViewBag.Cedula = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Cedula", "PERSONA.Nombre");
            return View();
        }

        // POST: Vacaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vacaciones vACACIONE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    Servicios se = new Servicios();

                    Usuario user = (Usuario)Session["User"];


                    vACACIONE.PERSONA.Cedula = user.PERSONA.Cedula;

                    ds = se.Validar_Dias_Vacaciones(vACACIONE.PERSONA.Cedula);

                    int.TryParse(ds.Tables[0].Rows[0]["DiasVacaciones"].ToString(), out int cantidadDias);

                    if (cantidadDias > 0)
                    {
                        ds = se.Crear_Vacaciones(vACACIONE);
                    }
                    else
                    {
                        Session["Error"] = "No tienes vacaciones disponibles";
                    }
                    
                    return RedirectToAction("VacacionesPersonal");
                }

                ViewBag.IdAprobaciones = new SelectList(db.aprobaciones, "IdAprobaciones", "Estado", vACACIONE.VACACION.Aprobaciones_IdAprobaciones);
                ViewBag.Cedula = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Cedula", "PERSONA.Nombre", vACACIONE.VACACION.Empleados_Id_Empleado);
                return View("VacacionesPersonal", vACACIONE);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("VacacionesPersonal");
            }
            
        }

        // GET: Vacaciones/Edit/5
        public ActionResult Edit(DateTime fechaInicio, int cantidadDias, int cedula, string estado, string comentario)
        {
            if (fechaInicio == null || cantidadDias == 0 || estado == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vacacion = db.vacaciones.FirstOrDefault(v => v.Fecha_Inicio == fechaInicio && v.Cantidad_Dias == cantidadDias);

            var viewModelList = new List<Vacaciones>();


            var aprobacion = db.aprobaciones.FirstOrDefault(u => u.IdAprobaciones == vacacion.Aprobaciones_IdAprobaciones);
            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == vacacion.Empleados_Id_Empleado);
            var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

            var viewModel = new Vacaciones
            {
                VACACION = vacacion,
                EMPLEADO = empleado,
                PERSONA = persona,
                APROBACION = aprobacion
            };

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdAprobaciones = new SelectList(db.aprobaciones, "IdAprobaciones", "Estado", viewModel.VACACION.Aprobaciones_IdAprobaciones);
            ViewBag.Cedula = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Cedula", "PERSONA.Nombre", viewModel.VACACION.Empleados_Id_Empleado);

            return View(viewModel);
        }

        // POST: Vacaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vacaciones vACACIONE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    Servicios se = new Servicios();

                    ds = se.Validar_Dias_Vacaciones(vACACIONE.PERSONA.Cedula);

                    int.TryParse(ds.Tables[0].Rows[0]["DiasVacaciones"].ToString(), out int cantidadDias);

                    if (cantidadDias > 0)
                    {
                        ds = se.Modificar_Vacaciones(vACACIONE);
                    }

                    return RedirectToAction("Index");
                }
                ViewBag.IdAprobaciones = new SelectList(db.aprobaciones, "IdAprobaciones", "Estado", vACACIONE.VACACION.Aprobaciones_IdAprobaciones);
                ViewBag.Cedula = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Cedula", "PERSONA.Nombre", vACACIONE.VACACION.Empleados_Id_Empleado);
                return View(vACACIONE);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Vacaciones/Delete/5
        public ActionResult Delete(DateTime fechaInicio, int cantidadDias, int cedula, string estado, string comentario)
        {
            if (fechaInicio == null || cantidadDias == 0 || cedula == 0 || estado == null || comentario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacacione vACACIONE = db.vacaciones.FirstOrDefault(v => v.Fecha_Inicio == fechaInicio && v.Cantidad_Dias == cantidadDias);
            if (vACACIONE == null)
            {
                return HttpNotFound();
            }
            return View(vACACIONE);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime fechaInicio, int cantidadDias, int cedula, string estado, string comentario)
        {
            try
            {
                vacacione vACACIONE = db.vacaciones.FirstOrDefault(v => v.Fecha_Inicio == fechaInicio && v.Cantidad_Dias == cantidadDias);
                DataSet ds = new DataSet();
                Servicios se = new Servicios();
                ds = se.Eliminar_Vacaciones(vACACIONE);

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
