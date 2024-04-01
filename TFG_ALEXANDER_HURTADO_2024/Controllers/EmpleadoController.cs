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
using DB.Models;

namespace DB.Models.Controllers
{
    public class EmpleadoController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Empleado
        public ActionResult Index()
        {
            var eMPLEADOS = db.empleados.Include(e => e.persona);
            return View(eMPLEADOS.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? idEmpleado, int? Cedula)
        {
            if (idEmpleado == null || Cedula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DB.Models.empleado eMPLEADO = db.empleados.Find(idEmpleado, Cedula);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.Id_Empleados = new SelectList(db.empleados, "Id_Empleados", "Id_Empleados");
            ViewBag.Id_Empleados = new SelectList(db.empleados, "Id_Empleados", "Id_Empleados");
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Empleados,Fecha_Ingreso,Salario,Cedula,Id_Administrador")] empleado eMPLEADO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.empleados.Add(eMPLEADO);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", eMPLEADO.Persona_Cedula);
                return View(eMPLEADO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? idEmpleado, int? Cedula)
        {
            if (idEmpleado == null || Cedula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DB.Models.empleado eMPLEADO = db.empleados.Find(idEmpleado, Cedula);

            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Empleados = new SelectList(db.empleados, "Id_Empleados", "Id_Empleados", eMPLEADO.Id_Empleado);
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", eMPLEADO.Persona_Cedula);
            return View(eMPLEADO);
        }


        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(DB.Models.empleado eMPLEADO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (RHEntities db2 = new RHEntities())
                    {

                        db2.Database.ExecuteSqlCommand("SP_EditEmpleado @Id_Empleados, @Fecha_Ingreso, @Salario, @Cedula, @Id_Administrador",
                                                                                               new SqlParameter("@Id_Empleados", eMPLEADO.Id_Empleado),
                                                                                               new SqlParameter("@Fecha_Ingreso", eMPLEADO.Fecha_Ingreso),
                                                                                               new SqlParameter("@Salario", eMPLEADO.Salario),
                                                                                               new SqlParameter("@Cedula", eMPLEADO.persona.Cedula),
                                                                                               new SqlParameter("@Id_Administrador", eMPLEADO.Jefatura_Id_Empleado));

                    }

                    return RedirectToAction("Index");
                }
                ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", eMPLEADO.Persona_Cedula);
                return View(eMPLEADO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }


        // GET: Empleado/Delete/5
        public ActionResult Delete(int? idEmpleado, int? Cedula)
        {
            if (idEmpleado == null || Cedula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DB.Models.empleado eMPLEADO = db.empleados.Find(idEmpleado, Cedula);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id_Empleados, int cedula)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SP_EliminarEmpleado @Id_Empleados, @Cedula",
                                              new SqlParameter("@Id_Empleados", id_Empleados),
                                              new SqlParameter("@Cedula", cedula));

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
