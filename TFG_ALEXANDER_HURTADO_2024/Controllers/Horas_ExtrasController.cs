using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.DBConnection;
using DB.Models;
using DB.Models.Cruds;
using Newtonsoft.Json;

namespace DB.Models.Controllers
{
    public class Horas_ExtrasController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Horas_Extras
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            Servicios se = new Servicios();
            ds = se.Get_Info_HorasExtras();

            List<Models.horas_extras> horasExtras = new List<Models.horas_extras>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                horas_extras horaExtra = new horas_extras();

                horaExtra.Id_Horas = Convert.ToInt32(row["Id_Horas"]);
                horaExtra.Fecha_Horas_Trabajadas = Convert.ToDateTime(row["Fecha_Horas_Trabajadas"]);
                horaExtra.Empleados_Id_Empleado = Convert.ToInt32(row["Empleados_Id_Empleado"]);
                horaExtra.Cantidad_Horas_Extras = Convert.ToInt32(row["Cantidad_Horas_Extras"]);
                float.TryParse(row["Monto"].ToString(), out float monto);
                horaExtra.Monto = monto;
                horaExtra.Catalago_Tipo_Horas_Extras_Id_Hora_Extra = Convert.ToInt32(row["Catalago_Tipo_Horas_Extras_Id_Hora_Extra"]);
                // Crear e inicializar EMPLEADO
                horaExtra.empleado = new Models.empleado();
                horaExtra.empleado.Salario = Convert.ToInt32(row["Salario"]);
                // Crear e inicializar PERSONA
                horaExtra.empleado.persona = new Models.persona();
                horaExtra.empleado.persona.Nombre = row["Nombre"].ToString() + " " + row["Apeliido1"].ToString() + " " + row["Apellido2"].ToString();

                // Crear e inicializar Catalogo_Control_permisos_Colaborador si es necesario
                horaExtra.catalago_tipo_horas_extras = new Models.catalago_tipo_horas_extras();
                horaExtra.catalago_tipo_horas_extras.Descripcion = row["Descripcion"].ToString();
                horaExtra.catalago_tipo_horas_extras.Porcentaje = Convert.ToDecimal(row["Porcentaje"]);
                horasExtras.Add(horaExtra);
            }

            return View(horasExtras);
        }

        public ActionResult HorasExtrasPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];
            var hORAS_EXTRAS = db.horas_extras.Include(h => h.catalago_tipo_horas_extras).Where(h => h.Empleados_Id_Empleado== user.EMPLEADO.Id_Empleado);
            return View(hORAS_EXTRAS.ToList());
        }

        // GET: Horas_Extras/Details/5
        public ActionResult Details(string id, int idEmpleado, int idTipoHoras)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime fechaParsed = DateTime.ParseExact(id, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            horas_extras hORAS_EXTRAS = db.horas_extras.FirstOrDefault(i => i.Fecha_Horas_Trabajadas == fechaParsed && i.Empleados_Id_Empleado == idEmpleado && i.Catalago_Tipo_Horas_Extras_Id_Hora_Extra == idTipoHoras);
            if (hORAS_EXTRAS == null)
            {
                return HttpNotFound();
            }
            return View(hORAS_EXTRAS);
        }

        // GET: Horas_Extras/Create
        public ActionResult Create()
        {
            ViewBag.Catalago_Tipo_Horas_Extras_Id_Hora_Extra = new SelectList(db.catalago_tipo_horas_extras, "Id_Hora_Extra", "Descripcion");

            // Obtener la lista de Cedulas y pasarla a la vista
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre");

            return View();
        }

        // POST: Horas_Extras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(horas_extras hORAS_EXTRAS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    catalago_tipo_horas_extras tipoHorasExtra = db.catalago_tipo_horas_extras.FirstOrDefault(c => c.Id_Hora_Extra == hORAS_EXTRAS.Catalago_Tipo_Horas_Extras_Id_Hora_Extra);
                    empleado empleado = db.empleados.FirstOrDefault(c => c.Id_Empleado == hORAS_EXTRAS.Empleados_Id_Empleado);

                    hORAS_EXTRAS.Monto = (float)((empleado.Salario * tipoHorasExtra.Porcentaje) * hORAS_EXTRAS.Cantidad_Horas_Extras);

                    db.horas_extras.Add(hORAS_EXTRAS);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Catalago_Tipo_Horas_Extras_Id_Hora_Extra = new SelectList(db.catalago_tipo_horas_extras, "Id_Hora_Extra", "Descripcion", hORAS_EXTRAS.Catalago_Tipo_Horas_Extras_Id_Hora_Extra);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", hORAS_EXTRAS.Empleados_Id_Empleado);
                return View(hORAS_EXTRAS);
            }
            catch (Exception ex)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Horas_Extras/Edit/5
        public ActionResult Edit(string id, int idEmpleado, int idTipoHoras)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime fechaParsed = DateTime.ParseExact(id, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            horas_extras hORAS_EXTRAS = db.horas_extras.FirstOrDefault(i => i.Fecha_Horas_Trabajadas == fechaParsed && i.Empleados_Id_Empleado == idEmpleado && i.Catalago_Tipo_Horas_Extras_Id_Hora_Extra == idTipoHoras);
            if (hORAS_EXTRAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.Catalago_Tipo_Horas_Extras_Id_Hora_Extra = new SelectList(db.catalago_tipo_horas_extras, "Id_Hora_Extra", "Descripcion", hORAS_EXTRAS.Catalago_Tipo_Horas_Extras_Id_Hora_Extra);
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", hORAS_EXTRAS.Empleados_Id_Empleado);
            return View(hORAS_EXTRAS);
        }

        // POST: Horas_Extras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(horas_extras hORAS_EXTRAS)
        {
            try
            {
                catalago_tipo_horas_extras tipoHorasExtra = db.catalago_tipo_horas_extras.FirstOrDefault(c => c.Id_Hora_Extra == hORAS_EXTRAS.Catalago_Tipo_Horas_Extras_Id_Hora_Extra);
                empleado empleado = db.empleados.FirstOrDefault(c => c.Id_Empleado == hORAS_EXTRAS.Empleados_Id_Empleado);

                hORAS_EXTRAS.Monto = (float)((empleado.Salario * tipoHorasExtra.Porcentaje) * hORAS_EXTRAS.Cantidad_Horas_Extras);
                if (ModelState.IsValid)
                {
                    db.Entry(hORAS_EXTRAS).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Catalago_Tipo_Horas_Extras_Id_Hora_Extra = new SelectList(db.catalago_tipo_horas_extras, "Id_Hora_Extra", "Descripcion", hORAS_EXTRAS.Catalago_Tipo_Horas_Extras_Id_Hora_Extra);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", hORAS_EXTRAS.Empleados_Id_Empleado);
                return View(hORAS_EXTRAS);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Horas_Extras/Delete/5
        public ActionResult Delete(string id, int idEmpleado, int idTipoHoras)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime fechaParsed = DateTime.ParseExact(id, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            horas_extras hORAS_EXTRAS = db.horas_extras.FirstOrDefault(i => i.Fecha_Horas_Trabajadas == fechaParsed && i.Empleados_Id_Empleado == idEmpleado && i.Catalago_Tipo_Horas_Extras_Id_Hora_Extra == idTipoHoras);
            if (hORAS_EXTRAS == null)
            {
                return HttpNotFound();
            }
            return View(hORAS_EXTRAS);
        }

        // POST: Horas_Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, int idEmpleado, int idTipoHoras)
        {
            try
            {
                DateTime fechaParsed = DateTime.ParseExact(id, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                horas_extras hORAS_EXTRAS = db.horas_extras.FirstOrDefault(i => i.Fecha_Horas_Trabajadas == fechaParsed && i.Empleados_Id_Empleado == idEmpleado && i.Catalago_Tipo_Horas_Extras_Id_Hora_Extra == idTipoHoras);
                db.horas_extras.Remove(hORAS_EXTRAS);
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
