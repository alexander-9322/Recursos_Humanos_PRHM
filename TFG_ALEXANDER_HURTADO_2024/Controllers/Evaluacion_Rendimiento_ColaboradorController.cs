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

namespace DB.Models.Controllers
{
    public class Evaluacion_Rendimiento_ColaboradorController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Evaluacion_Rendimiento_Colaborador
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            Servicios se = new Servicios();
            ds = se.Get_Info_Evaluacion();

            List<Models.evaluacion_remndimiento_colaborador> evaluacionList = new List<Models.evaluacion_remndimiento_colaborador>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                evaluacion_remndimiento_colaborador evaluacion = new evaluacion_remndimiento_colaborador();

                evaluacion.Id_Evaluacion = Convert.ToInt32(row["Id_Evaluacion"]);
                evaluacion.Fecha_Evaluacion = Convert.ToDateTime(row["Fecha_Evaluacion"]);
                evaluacion.Empleados_Id_Empleado = Convert.ToInt32(row["Empleados_Id_Empleado"]);
                evaluacion.Catalago_Desempeño_Id_Desempeño = Convert.ToInt32(row["Catalago_Desempeño_Id_Desempeño"]);
                evaluacion.Calificacion = Convert.ToInt32(row["Calificacion"]);
                evaluacion.Retroalimentacion = row["Retroalimentacion"].ToString();
                // Crear e inicializar EMPLEADO
                evaluacion.empleado = new Models.empleado();
                // Crear e inicializar PERSONA
                evaluacion.empleado.persona = new Models.persona();
                evaluacion.empleado.persona.Nombre = row["Nombre"].ToString() + " " + row["Apeliido1"].ToString() + " " + row["Apellido2"].ToString();

                // Crear e inicializar Catalogo_Control_permisos_Colaborador si es necesario
                evaluacion.catalago_desempeño = new Models.catalago_desempeño();
                evaluacion.catalago_desempeño.Descripcion = row["Descripcion"].ToString();
                evaluacion.catalago_desempeño.Id_Desempeño = Convert.ToInt32(row["Id_Desempeño"]);
                evaluacionList.Add(evaluacion);
            }

            return View(evaluacionList);
        }

        public ActionResult RendimientoPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];
            var eVALUCION_RENDIMIENTO_COLABORADOR = db.evaluacion_remndimiento_colaborador.Include(e => e.catalago_desempeño).Where(e => e.Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);

            return View(eVALUCION_RENDIMIENTO_COLABORADOR.ToList());
        }

        // GET: Evaluacion_Rendimiento_Colaborador/Details/5
        public ActionResult Details(int? id, int? idCatalogoDesempeno, int? idEmpleado)
        {
            if (id == null || idCatalogoDesempeno == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evaluacion_remndimiento_colaborador eVALUCION_RENDIMIENTO_COLABORADOR = db.evaluacion_remndimiento_colaborador.FirstOrDefault(cdp => cdp.Id_Evaluacion == id && cdp.Empleados_Id_Empleado == idEmpleado && cdp.Catalago_Desempeño_Id_Desempeño == idCatalogoDesempeno);
            if (eVALUCION_RENDIMIENTO_COLABORADOR == null)
            {
                return HttpNotFound();
            }
            return View(eVALUCION_RENDIMIENTO_COLABORADOR);
        }

        // GET: Evaluacion_Rendimiento_Colaborador/Create
        public ActionResult Create()
        {
            ViewBag.Catalago_Desempeño_Id_Desempeño= new SelectList(db.catalago_desempeño, "Id_Desempeño", "Descripcion");
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre");
            return View();
        }

        // POST: Evaluacion_Rendimiento_Colaborador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(evaluacion_remndimiento_colaborador eVALUCION_RENDIMIENTO_COLABORADOR)
        {
            try
            {

                eVALUCION_RENDIMIENTO_COLABORADOR.Fecha_Evaluacion = DateTime.Now;
                eVALUCION_RENDIMIENTO_COLABORADOR.Calificacion = (eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta1 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta2 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta3 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta4 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta5 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta6 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta7 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta8 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta9 +
                                          eVALUCION_RENDIMIENTO_COLABORADOR.Pregunta10 ) / 10;

                eVALUCION_RENDIMIENTO_COLABORADOR.Catalago_Desempeño_Id_Desempeño = (int)eVALUCION_RENDIMIENTO_COLABORADOR.Calificacion;

                if (ModelState.IsValid)
                {
                    evaluacion_remndimiento_colaborador tieneEvaluacion = db.evaluacion_remndimiento_colaborador.FirstOrDefault(l => l.Empleados_Id_Empleado == eVALUCION_RENDIMIENTO_COLABORADOR.Empleados_Id_Empleado);

                    if (tieneEvaluacion == null)
                    {
                        db.evaluacion_remndimiento_colaborador.Add(eVALUCION_RENDIMIENTO_COLABORADOR);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        DateTime fechaActual = DateTime.Now;

                        int diferenciaMeses = ((fechaActual.Year - tieneEvaluacion.Fecha_Evaluacion.Year) * 12) + fechaActual.Month - tieneEvaluacion.Fecha_Evaluacion.Month;

                        if (diferenciaMeses >= 6)
                        {
                            db.evaluacion_remndimiento_colaborador.Add(eVALUCION_RENDIMIENTO_COLABORADOR);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Session["Error"] = "El usuario ya tiene una evaluación dentro de los 6 meses estimados";
                        }
                    }


                }


                ViewBag.Catalago_Desempeño_Id_Desempeño= new SelectList(db.catalago_desempeño, "Id_Desempeño", "Descripcion", eVALUCION_RENDIMIENTO_COLABORADOR.Catalago_Desempeño_Id_Desempeño);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", eVALUCION_RENDIMIENTO_COLABORADOR.Empleados_Id_Empleado);
                return View(eVALUCION_RENDIMIENTO_COLABORADOR);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Evaluacion_Rendimiento_Colaborador/Edit/5
        public ActionResult Edit(int? id, int? idCatalogoDesempeno, int? idEmpleado)
        {
            if (id == null || idCatalogoDesempeno == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evaluacion_remndimiento_colaborador eVALUCION_RENDIMIENTO_COLABORADOR = db.evaluacion_remndimiento_colaborador.FirstOrDefault(cdp => cdp.Id_Evaluacion == id && cdp.Empleados_Id_Empleado == idEmpleado && cdp.Catalago_Desempeño_Id_Desempeño == idCatalogoDesempeno);
            if (eVALUCION_RENDIMIENTO_COLABORADOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.Catalago_Desempeño_Id_Desempeño= new SelectList(db.catalago_desempeño, "Id_Desempeño", "Descripcion", eVALUCION_RENDIMIENTO_COLABORADOR.Catalago_Desempeño_Id_Desempeño);
             ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", eVALUCION_RENDIMIENTO_COLABORADOR.Empleados_Id_Empleado);
            return View(eVALUCION_RENDIMIENTO_COLABORADOR);
        }

        // POST: Evaluacion_Rendimiento_Colaborador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(evaluacion_remndimiento_colaborador eVALUCION_RENDIMIENTO_COLABORADOR)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(eVALUCION_RENDIMIENTO_COLABORADOR).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Catalago_Desempeño_Id_Desempeño= new SelectList(db.catalago_desempeño, "Id_Desempeño", "Descripcion", eVALUCION_RENDIMIENTO_COLABORADOR.Catalago_Desempeño_Id_Desempeño);
                 ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", eVALUCION_RENDIMIENTO_COLABORADOR.Empleados_Id_Empleado);
                return View(eVALUCION_RENDIMIENTO_COLABORADOR);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Evaluacion_Rendimiento_Colaborador/Delete/5
        public ActionResult Delete(int? id, int? idCatalogoDesempeno, int? idEmpleado)
        {
            if (id == null || idCatalogoDesempeno == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evaluacion_remndimiento_colaborador eVALUCION_RENDIMIENTO_COLABORADOR = db.evaluacion_remndimiento_colaborador.FirstOrDefault(cdp => cdp.Id_Evaluacion == id && cdp.Empleados_Id_Empleado == idEmpleado && cdp.Catalago_Desempeño_Id_Desempeño == idCatalogoDesempeno);
            if (eVALUCION_RENDIMIENTO_COLABORADOR == null)
            {
                return HttpNotFound();
            }
            return View(eVALUCION_RENDIMIENTO_COLABORADOR);
        }

        // POST: Evaluacion_Rendimiento_Colaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? idCatalogoDesempeno, int? idEmpleado)
        {
            try
            {
                evaluacion_remndimiento_colaborador eVALUCION_RENDIMIENTO_COLABORADOR = db.evaluacion_remndimiento_colaborador.FirstOrDefault(cdp => cdp.Id_Evaluacion == id && cdp.Empleados_Id_Empleado == idEmpleado && cdp.Catalago_Desempeño_Id_Desempeño == idCatalogoDesempeno);
                db.evaluacion_remndimiento_colaborador.Remove(eVALUCION_RENDIMIENTO_COLABORADOR);
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
