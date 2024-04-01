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
using DB.Models.Pagos;
using DB.Models.Usuarios;

namespace TFG_ALEXANDER_HURTADO_2024.Controllers
{
    public class PlanillaController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Planilla
        public ActionResult Index()
        {
            var planillas = db.planillas;

            var viewModelList = new List<Planilla>();

            foreach (var planilla in planillas)
            {
                var detalles = db.detalles_planilla.FirstOrDefault(u => u.Planilla_Id_Planilla == planilla.Id_Planilla);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == planilla.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); 
                var deducciones = db.catalago_deducciones.FirstOrDefault(u => u.idCatalago_Deducciones == planilla.Catalago_Deducciones_idCatalago_Deducciones); 

                var viewModel = new Planilla
                {
                    DETALLES_PLANILLA = detalles,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    DEDUCCIONES = deducciones,
                    PLANILLA = planilla
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }
        

        public ActionResult PlanillaPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];

            var planillas = db.planillas.Where(p => p.Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);

            var viewModelList = new List<Planilla>();

            foreach (var planilla in planillas)
            {
                var detalles = db.detalles_planilla.FirstOrDefault(u => u.Planilla_Id_Planilla == planilla.Id_Planilla);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == planilla.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula);
                var deducciones = db.catalago_deducciones.FirstOrDefault(u => u.idCatalago_Deducciones == planilla.Catalago_Deducciones_idCatalago_Deducciones);

                var viewModel = new Planilla
                {
                    DETALLES_PLANILLA = detalles,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    DEDUCCIONES = deducciones,
                    PLANILLA = planilla
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        // GET: Planilla/Details/5
        public ActionResult Details(int? id, int? idCatalogo, int? idCatalogo2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var planilla = db.planillas.FirstOrDefault(p => p.Id_Planilla == id && p.Catalago_Deducciones_idCatalago_Deducciones == idCatalogo);

            var detalles = db.detalles_planilla.FirstOrDefault(u => u.Planilla_Id_Planilla == planilla.Id_Planilla);
            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == planilla.Empleados_Id_Empleado);
            var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula);
            var deducciones = db.catalago_deducciones.FirstOrDefault(u => u.idCatalago_Deducciones == planilla.Catalago_Deducciones_idCatalago_Deducciones);

            var viewModel = new Planilla
            {
                DETALLES_PLANILLA = detalles,
                EMPLEADO = empleado,
                PERSONA = persona,
                DEDUCCIONES = deducciones,
                PLANILLA = planilla
            };


            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Planilla/Create
        public ActionResult Create()
        {
            ViewBag.Catalago_Deducciones_idCatalago_Deducciones = new SelectList(db.catalago_deducciones, "idCatalago_Deducciones", "Descripcion");
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
            return View();
        }

        // POST: Planilla/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Planilla _objplanilla)
        {
            try
            {
                DataSet ds = new DataSet();
                Servicios se = new Servicios();
                if (ModelState.IsValid)
                {
                    ds = se.CalcularPago_Planilla_X_Empleado(_objplanilla);
                    return RedirectToAction("Index");
                }

                ViewBag.Catalago_Deducciones_idCatalago_Deducciones = new SelectList(db.catalago_deducciones, "idCatalago_Deducciones", "Descripcion");
                ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
                return View(_objplanilla);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult GenerarPlanillas()
        {
            try
            {
                var empleados = db.empleados.ToList();

                foreach (var empleado in empleados)
                {
                    DataSet ds = new DataSet();
                    Servicios se = new Servicios();
                    Planilla planilla = new Planilla();

                    var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula);

                    planilla.PERSONA.Cedula = persona.Cedula;
                    planilla.PLANILLA.Horas_Laboradas = 48;
                    planilla.PLANILLA.Catalago_Deducciones_idCatalago_Deducciones = 1;
                    ds = se.CalcularPago_Planilla_X_Empleado(planilla);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudieron crear las planillas";
                return RedirectToAction("Index");
            }
        }

        // GET: Planilla/Edit/5
        public ActionResult Edit(int? id, int? idCatalogo, int? idCatalogo2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var planilla = db.planillas.FirstOrDefault(p => p.Id_Planilla == id && p.Catalago_Deducciones_idCatalago_Deducciones == idCatalogo);

            var detalles = db.detalles_planilla.FirstOrDefault(u => u.Planilla_Id_Planilla == planilla.Id_Planilla);
            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == planilla.Empleados_Id_Empleado);
            var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula);
            var deducciones = db.catalago_deducciones.FirstOrDefault(u => u.idCatalago_Deducciones == planilla.Catalago_Deducciones_idCatalago_Deducciones);

            var viewModel = new Planilla
            {
                DETALLES_PLANILLA = detalles,
                EMPLEADO = empleado,
                PERSONA = persona,
                DEDUCCIONES = deducciones,
                PLANILLA = planilla
            };

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Catalago_Deducciones_idCatalago_Deducciones = new SelectList(db.catalago_deducciones, "idCatalago_Deducciones", "Descripcion");
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
            return View(viewModel);
        }

        // POST: Planilla/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Planilla pLANILLA)
        {
            try
            {
                DataSet ds = new DataSet();
                Servicios se = new Servicios();
                if (ModelState.IsValid)
                {
                    ds = se.Editar_CalcularPago_Planilla_X_Empleado(pLANILLA);
                    return RedirectToAction("Index");
                }
                ViewBag.Catalago_Deducciones_idCatalago_Deducciones = new SelectList(db.catalago_deducciones, "idCatalago_Deducciones", "Descripcion");
                ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
                return View(pLANILLA);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
           
        }

        // GET: Planilla/Delete/5
        public ActionResult Delete(int? id, int? idCatalogo, int? idDetalles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var planilla = db.planillas.FirstOrDefault(p => p.Id_Planilla == id && p.Catalago_Deducciones_idCatalago_Deducciones == idCatalogo);

            var detalles = db.detalles_planilla.FirstOrDefault(u => u.Planilla_Id_Planilla == planilla.Id_Planilla);
            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == planilla.Empleados_Id_Empleado);
            var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula);
            var deducciones = db.catalago_deducciones.FirstOrDefault(u => u.idCatalago_Deducciones == planilla.Catalago_Deducciones_idCatalago_Deducciones);

            var viewModel = new Planilla
            {
                DETALLES_PLANILLA = detalles,
                EMPLEADO = empleado,
                PERSONA = persona,
                DEDUCCIONES = deducciones,
                PLANILLA = planilla
            };

            return View(viewModel);
        
        }

        // POST: Planilla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? idCatalogo, int? idDetalles)
        {
            try
            {
                detalles_planilla detalles = db.detalles_planilla.FirstOrDefault(p => p.idDetalles_Planilla == idDetalles);
                db.detalles_planilla.Remove(detalles);
                planilla pLANILLA = db.planillas.FirstOrDefault(p => p.Id_Planilla == id && p.Catalago_Deducciones_idCatalago_Deducciones == idCatalogo);
                db.planillas.Remove(pLANILLA);
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
