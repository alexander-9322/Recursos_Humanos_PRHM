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
using DB.Models.Cruds;
using DB.Models.Usuarios;

namespace DB.Models.Controllers
{
    public class Calculo_AguinaldoController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Calculo_Aguinaldo
        public ActionResult Index()
        {
            var aguinaldos = db.calculo_aguinaldo.ToList();

            var viewModelList = new List<Aguinaldo>();

            foreach (var aguinaldo in aguinaldos)
            {
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == aguinaldo.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Aguinaldo
                {
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    AGUINALDO = aguinaldo
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        public ActionResult AguinaldoPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];

            var aguinaldos = db.calculo_aguinaldo.Where(c => c.Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);

            var viewModelList = new List<Aguinaldo>();

            foreach (var aguinaldo in aguinaldos)
            {
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == aguinaldo.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Aguinaldo
                {
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    AGUINALDO = aguinaldo
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        // GET: Calculo_Aguinaldo/Details/5
        public ActionResult Details(int? idAguinaldo, int? idEmpleado)
        {
            if (idAguinaldo == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.calculo_aguinaldo cALCULO_AGUINALDO = db.calculo_aguinaldo.Find(idAguinaldo, idEmpleado);
            if (cALCULO_AGUINALDO == null)
            {
                return HttpNotFound();
            }
            return View(cALCULO_AGUINALDO);
        }

        // GET: Calculo_Aguinaldo/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
            return View();
        }


        // POST: Calculo_Aguinaldo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(persona persona)
        {
            try
            {
                DataSet ds = new DataSet();
                Servicios se = new Servicios();

                if (ModelState.IsValid)
                {
                    ds = se.Calcular_Aguinaldo(persona);
                    return RedirectToAction("Index");
                }

                ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", persona.Cedula);
                return View(persona);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult GenerarAguinaldos()
        {
            try
            {
                var empleados = db.empleados.ToList();

                foreach (var empleado in empleados)
                {
                    DataSet ds = new DataSet();
                    Servicios se = new Servicios();

                    var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula);

                    persona _persona = new persona
                    {
                        Cedula = persona.Cedula
                    };

                    ds = se.Calcular_Aguinaldo(_persona);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudieron crear las planillas";
                return RedirectToAction("Index");
            }
        }

        // GET: Calculo_Aguinaldo/Edit/5
        // GET: Calculo_Aguinaldo/Edit/5
        public ActionResult Edit(int? idAguinaldo, int? idEmpleado)
        {
            if (idAguinaldo == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aguinaldo = db.calculo_aguinaldo.Find(idAguinaldo, idEmpleado);

            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == idEmpleado);
            var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

            var viewModel = new Aguinaldo
            {
                EMPLEADO = empleado,
                PERSONA = persona,
                AGUINALDO = aguinaldo
            };

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Empleados = new SelectList(db.empleados, "Id_Empleados", "Id_Empleados", viewModel.AGUINALDO.Empleados_Id_Empleado);
            ViewBag.Cedula = new SelectList(db.empleados, "Cedula", "Cedula", viewModel.PERSONA.Cedula);

            return View(viewModel);
        }

        // POST: Calculo_Aguinaldo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Aguinaldo cALCULO_AGUINALDO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (RHEntities db2 = new RHEntities())
                    {
                        db2.Database.ExecuteSqlCommand("SP_EditCalculoAguinaldo @Id_Aguinaldo, @Anno, @Dias_Trabajados, @Total_Salarios, @Aguinaldo, @Id_Empleados, @Cedula",
                                                                                                            new SqlParameter("@Id_Aguinaldo", cALCULO_AGUINALDO.AGUINALDO.Id_Aguinaldo),
                                                                                                            new SqlParameter("@Anno", cALCULO_AGUINALDO.AGUINALDO.Año),
                                                                                                            new SqlParameter("@Dias_Trabajados", cALCULO_AGUINALDO.AGUINALDO.Dias_Trabajados),
                                                                                                            new SqlParameter("@Total_Salarios", cALCULO_AGUINALDO.AGUINALDO.Total_Salarios),
                                                                                                            new SqlParameter("@Aguinaldo", cALCULO_AGUINALDO.AGUINALDO.Aguinaldo),
                                                                                                            new SqlParameter("@Id_Empleados", cALCULO_AGUINALDO.AGUINALDO.Empleados_Id_Empleado),
                                                                                                            new SqlParameter("@Cedula", cALCULO_AGUINALDO.PERSONA.Cedula));

                    }
                    return RedirectToAction("Index");
                }

                ViewBag.Id_Empleados = new SelectList(db.empleados, "Id_Empleados", "Id_Empleados", cALCULO_AGUINALDO.AGUINALDO.Empleados_Id_Empleado);
                ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Cedula", cALCULO_AGUINALDO.PERSONA.Cedula);

                return View(cALCULO_AGUINALDO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Calculo_Aguinaldo/Delete/5
        public ActionResult Delete(int? idAguinaldo, int? idEmpleado)
        {
            if (idAguinaldo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DB.Models.calculo_aguinaldo cALCULO_AGUINALDO = db.calculo_aguinaldo.FirstOrDefault(a => a.Id_Aguinaldo == idAguinaldo && a.Empleados_Id_Empleado == idEmpleado);
            if (cALCULO_AGUINALDO == null)
            {
                return HttpNotFound();
            }
            return View(cALCULO_AGUINALDO);
        }

        // POST: Calculo_Aguinaldo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idAguinaldo)
        {
            DataSet ds = new DataSet();
            Servicios se = new Servicios();
            calculo_aguinaldo ca = new calculo_aguinaldo();
            ca.Id_Aguinaldo = idAguinaldo;
            try
            {
                ds = se.Eliminar_Aguinaldo(ca);

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
