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
    public class LiquidacionController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Liquidacion
        public ActionResult Index()
        {
            var detalles_liquidaciones = db.detalles_liquidacion;

            var viewModelList = new List<Liquidacion>();

            foreach (var detalles_liquidacion in detalles_liquidaciones)
            {
                var liquidacion = db.liquidacions.FirstOrDefault(u => u.Id_Liquidacion == detalles_liquidacion.Liquidacion_Id_Liquidacion);
                var catalogo = db.catalago_tipo_liquidacion.FirstOrDefault(u => u.Id_Tipo_Liquidacion == liquidacion.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == liquidacion.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Liquidacion
                {
                    DETALLES_LIQUIDACION = detalles_liquidacion,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    TIPO_LIQUDACION = catalogo,
                    LIQUIDACION = liquidacion
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        public ActionResult LiquidacionPersonal()
        {
            DB.Models.Usuarios.Usuario user = (DB.Models.Usuarios.Usuario)Session["User"];

            var detalles_liquidaciones = db.detalles_liquidacion.Where(l => l.Liquidacion_Empleados_Id_Empleado == user.EMPLEADO.Id_Empleado);

            var viewModelList = new List<Liquidacion>();

            foreach (var detalles_liquidacion in detalles_liquidaciones)
            {
                var liquidacion = db.liquidacions.FirstOrDefault(u => u.Id_Liquidacion == detalles_liquidacion.Liquidacion_Id_Liquidacion);
                var catalogo = db.catalago_tipo_liquidacion.FirstOrDefault(u => u.Id_Tipo_Liquidacion == liquidacion.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion);
                var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == liquidacion.Empleados_Id_Empleado);
                var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

                var viewModel = new Liquidacion
                {
                    DETALLES_LIQUIDACION = detalles_liquidacion,
                    EMPLEADO = empleado,
                    PERSONA = persona,
                    TIPO_LIQUDACION = catalogo,
                    LIQUIDACION = liquidacion
                };

                viewModelList.Add(viewModel);
            }


            return View(viewModelList);
        }

        // GET: Liquidacion/Details/5
        public ActionResult Details(int? idLiquidacion, int? idEmpleado)
        {
            if (idLiquidacion == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.liquidacion lIQUIDACION = db.liquidacions.Find(idLiquidacion, idEmpleado);
            if (lIQUIDACION == null)
            {
                return HttpNotFound();
            }
            return View(lIQUIDACION);
        }

        // GET: Liquidacion/Create
        public ActionResult Create()
        {
            ViewBag.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion = new SelectList(db.catalago_tipo_liquidacion, "Id_Tipo_Liquidacion", "Descripcion");
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre");
            return View();
        }

        // POST: Liquidacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Liquidacion _objLiquidacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    Servicios se = new Servicios();

                    ds = se.Obtener_Id_Detalle_Liquidacion();
                    int.TryParse(ds.Tables[0].Rows[0]["IdDetalleLiquidacion"].ToString(), out int id_Liquidacion);


                    liquidacion _liquidacion = new liquidacion();
                    _liquidacion.Fecha_salida = _objLiquidacion.LIQUIDACION.Fecha_salida;
                    _liquidacion.Dias_Preaviso = _objLiquidacion.LIQUIDACION.Dias_Preaviso;
                    _liquidacion.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion = _objLiquidacion.LIQUIDACION.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion;
                    _liquidacion.Empleados_Id_Empleado = _objLiquidacion.LIQUIDACION.Empleados_Id_Empleado;
                    _liquidacion.Id_Tipo_Liquidacion = _objLiquidacion.LIQUIDACION.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion;
                    _liquidacion.Dias_Preaviso = CalcularDiasPreaviso(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado);

                    detalles_liquidacion _detallesLiquidaciones = new detalles_liquidacion();

                    _detallesLiquidaciones.Id_Detalle_Liquidacion = _objLiquidacion.DETALLES_LIQUIDACION.Id_Detalle_Liquidacion;

                    float.TryParse(CalcularMontosVacaciones(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado).ToString(), out float montoVacaciones);
                    _detallesLiquidaciones.Monto_Vacaciones = montoVacaciones;


                    float.TryParse(CalcularMontoCesantia(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado).ToString(), out float montoCesantia);
                    _detallesLiquidaciones.Monto_Censantia = montoCesantia;


                    float.TryParse(CalcularMontoAguinaldo(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado).ToString(), out float montoAguinaldo);
                    _detallesLiquidaciones.Monto_Aguinaldo = montoAguinaldo;

                    float montoPreaviso = 0;

                    if (_objLiquidacion.HizoPreaviso == 0)
                    {
                        _detallesLiquidaciones.Monto_Preaviso = 0;
                    }
                    if (_objLiquidacion.HizoPreaviso == 1)
                    {
                        float.TryParse(CalcularMontosPreaviso(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado, _liquidacion.Dias_Preaviso).ToString(), out montoPreaviso);
                        _detallesLiquidaciones.Monto_Preaviso = montoPreaviso;
                    }


                    _detallesLiquidaciones.Total_Liquidacion = montoPreaviso + montoAguinaldo + montoCesantia + montoVacaciones;

                    _liquidacion.Monto = (decimal)_detallesLiquidaciones.Total_Liquidacion;


                    _liquidacion.Id_Liquidacion = id_Liquidacion;
                    _detallesLiquidaciones.Liquidacion_Empleados_Id_Empleado = _objLiquidacion.LIQUIDACION.Empleados_Id_Empleado;

                    liquidacion tieneLiquidacion = db.liquidacions.FirstOrDefault(l => l.Empleados_Id_Empleado == _liquidacion.Empleados_Id_Empleado);

                    if (tieneLiquidacion == null)
                    {
                        db.liquidacions.Add(_liquidacion);
                        db.SaveChanges();

                        _detallesLiquidaciones.Liquidacion_Id_Liquidacion = _liquidacion.Id_Liquidacion;

                        db.detalles_liquidacion.Add(_detallesLiquidaciones);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Session["Error"] = "El usuario ya tiene una liquidación";
                    }
                }

                ViewBag.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion = new SelectList(db.catalago_tipo_liquidacion, "Id_Tipo_Liquidacion", "Descripcion", _objLiquidacion.LIQUIDACION.Id_Tipo_Liquidacion);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", _objLiquidacion.LIQUIDACION.Empleados_Id_Empleado);
                return View(_objLiquidacion);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }

        }

        private decimal CalcularMontoAguinaldo(int idEmpleado)
        {
            try
            {
                var aguinaldo = db.calculo_aguinaldo.FirstOrDefault(u => u.Empleados_Id_Empleado == idEmpleado);

                if (aguinaldo != null)
                {
                    return aguinaldo.Aguinaldo;
                }
                else
                {
                    // Puedes manejar aquí qué hacer cuando aguinaldo es null
                    return 0;
                }
            }
            catch (Exception)
            {
                // Aquí también puedes decidir qué hacer en caso de excepción
                return 0;
            }


        }

        private decimal CalcularMontosVacaciones(int idEmpleado)
        {

            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == idEmpleado);
            var persona = db.personas.FirstOrDefault(p => p.Cedula == empleado.Persona_Cedula);

            DataSet ds = new DataSet();
            Servicios se = new Servicios();

            ds = se.Validar_Dias_Vacaciones(persona.Cedula);
            int.TryParse(ds.Tables[0].Rows[0]["DiasVacaciones"].ToString(), out int cantidadDias);

            decimal monto = (empleado.Salario * 8) * cantidadDias;

            return monto;

        }

        private int CalcularDiasPreaviso(int idEmpleado)
        {
            int diasPreaviso = 0;

            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == idEmpleado);

            DateTime fechaIngreso = empleado.Fecha_Ingreso;
            DateTime fechaActual = DateTime.Today;

            TimeSpan tiempoLaborado = fechaActual - fechaIngreso;
            int añosLaborados = tiempoLaborado.Days / 365;



            TimeSpan diasTranscurridos = fechaActual - fechaIngreso;
            int cantidadDias = diasTranscurridos.Days;

            if (cantidadDias < 90)
            {
                diasPreaviso = 0;
            }
            else if (cantidadDias >= 90 && cantidadDias <= 180)
            {
                diasPreaviso = 7;
            }
            else if (cantidadDias > 180 && cantidadDias <= 365)
            {
                diasPreaviso = 15;
            }
            else if (cantidadDias > 365)
            {
                diasPreaviso = 30;
            }

            return diasPreaviso;
        }

        private decimal CalcularMontoCesantia(int idEmpleado)
        {
            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == idEmpleado);

            DateTime fechaIngreso = empleado.Fecha_Ingreso;
            DateTime fechaActual = DateTime.Today;

            TimeSpan tiempoLaborado = fechaActual - fechaIngreso;
            int añosLaborados = tiempoLaborado.Days / 365;

            decimal salarioDiario = empleado.Salario; // Suponiendo un mes de 30 días

            decimal monto = 0;


            TimeSpan diasTranscurridos = fechaActual - fechaIngreso;
            int cantidadDias = diasTranscurridos.Days;

            if (cantidadDias < 90)
            {
                monto = 0 * (8 * salarioDiario);
            }

            if (cantidadDias > 90 && cantidadDias < 180)
            {
                monto = 7 * (8 * salarioDiario);
            }

            if (cantidadDias > 180 && cantidadDias < 365)
            {
                monto = 14 * (8 * salarioDiario);
            }

            if (añosLaborados >= 1 && añosLaborados < 2)
            {
                monto = 19.5m * (8 * salarioDiario);
            }
            else if (añosLaborados >= 2 && añosLaborados < 3)
            {
                monto = 20m * (8 * salarioDiario);
            }
            else if (añosLaborados >= 3 && añosLaborados < 4)
            {
                monto = 20.5m * (8 * salarioDiario);
            }
            else if (añosLaborados >= 4 && añosLaborados < 5)
            {
                monto = 21m * (8 * salarioDiario);
            }
            else if (añosLaborados >= 5 && añosLaborados < 6)
            {
                monto = 21.5m * (8 * salarioDiario);
            }
            else if (añosLaborados >= 6 && añosLaborados < 8)
            {
                monto = 22m * (8 * salarioDiario);
            }
            else if (añosLaborados >= 8)
            {
                monto = 22m * (8 * salarioDiario);
            }

            return monto;
        }


        private decimal CalcularMontosPreaviso(int idEmpleado, int dias)
        {

            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == idEmpleado);


            decimal monto = (empleado.Salario * 8) * dias;

            return monto;

        }

        // GET: Liquidacion/Edit/5
        public ActionResult Edit(int? idLiquidacion, int? idEmpleado)
        {
            if (idLiquidacion == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detalles_liquidacion = db.detalles_liquidacion.FirstOrDefault(i => i.Liquidacion_Id_Liquidacion == idLiquidacion && i.Liquidacion_Empleados_Id_Empleado == idEmpleado);
            var liquidacion = db.liquidacions.FirstOrDefault(u => u.Id_Liquidacion == detalles_liquidacion.Id_Detalle_Liquidacion);
            var catalogo = db.catalago_tipo_liquidacion.FirstOrDefault(u => u.Id_Tipo_Liquidacion == liquidacion.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion);
            var empleado = db.empleados.FirstOrDefault(u => u.Id_Empleado == liquidacion.Empleados_Id_Empleado);
            var persona = db.personas.FirstOrDefault(u => u.Cedula == empleado.Persona_Cedula); // Obtener el usuario asociado al empleado

            var viewModel = new Liquidacion
            {
                DETALLES_LIQUIDACION = detalles_liquidacion,
                EMPLEADO = empleado,
                PERSONA = persona,
                TIPO_LIQUDACION = catalogo,
                LIQUIDACION = liquidacion
            };

            if (viewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion = new SelectList(db.catalago_tipo_liquidacion, "Id_Tipo_Liquidacion", "Descripcion", viewModel.LIQUIDACION.Id_Tipo_Liquidacion);
            ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", viewModel.LIQUIDACION.Empleados_Id_Empleado);
            return View(viewModel);
        }

        // POST: Liquidacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Liquidacion _objLiquidacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        liquidacion _liquidacion = new liquidacion();
                        _liquidacion.Id_Liquidacion = _objLiquidacion.LIQUIDACION.Id_Liquidacion;
                        _liquidacion.Fecha_salida = _objLiquidacion.LIQUIDACION.Fecha_salida;
                        _liquidacion.Dias_Preaviso = _objLiquidacion.LIQUIDACION.Dias_Preaviso;
                        _liquidacion.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion = _objLiquidacion.LIQUIDACION.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion;
                        _liquidacion.Empleados_Id_Empleado = _objLiquidacion.LIQUIDACION.Empleados_Id_Empleado;
                        _liquidacion.Dias_Preaviso = CalcularDiasPreaviso(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado);



                        detalles_liquidacion _detallesLiquidaciones = new detalles_liquidacion();

                        _detallesLiquidaciones.Id_Detalle_Liquidacion = _objLiquidacion.DETALLES_LIQUIDACION.Id_Detalle_Liquidacion;

                        float.TryParse(CalcularMontosVacaciones(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado).ToString(), out float montoVacaciones);
                        _detallesLiquidaciones.Monto_Vacaciones = montoVacaciones;


                        float.TryParse(CalcularMontoCesantia(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado).ToString(), out float montoCesantia);
                        _detallesLiquidaciones.Monto_Censantia = montoCesantia;


                        float.TryParse(CalcularMontoAguinaldo(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado).ToString(), out float montoAguinaldo);
                        _detallesLiquidaciones.Monto_Aguinaldo = montoAguinaldo;

                        float montoPreaviso = 0;

                        if (_objLiquidacion.HizoPreaviso == 0)
                        {
                            _detallesLiquidaciones.Monto_Preaviso = 0;
                        }
                        if (_objLiquidacion.HizoPreaviso == 1)
                        {
                            float.TryParse(CalcularMontosPreaviso(_objLiquidacion.LIQUIDACION.Empleados_Id_Empleado, _objLiquidacion.LIQUIDACION.Dias_Preaviso).ToString(), out montoPreaviso);
                            _detallesLiquidaciones.Monto_Preaviso = montoPreaviso;
                        }


                        _detallesLiquidaciones.Total_Liquidacion = montoPreaviso + montoAguinaldo + montoCesantia + montoVacaciones;

                        _liquidacion.Monto = (decimal)_detallesLiquidaciones.Total_Liquidacion;


                        _detallesLiquidaciones.Liquidacion_Id_Liquidacion = _objLiquidacion.LIQUIDACION.Id_Liquidacion;
                        _detallesLiquidaciones.Liquidacion_Empleados_Id_Empleado = _objLiquidacion.LIQUIDACION.Empleados_Id_Empleado;

                        db.Entry(_liquidacion).State = EntityState.Modified;
                        db.SaveChanges();
                        db.Entry(_detallesLiquidaciones).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según tus necesidades
                        Console.WriteLine(ex.Message);
                        ModelState.AddModelError(string.Empty, "Error al editar la liquidación: " + ex.Message);
                        return View(_objLiquidacion);
                    }

                }

                ViewBag.Catalago_Tipo_Liquidacion_Id_Tipo_Liquidacion = new SelectList(db.catalago_tipo_liquidacion, "Id_Tipo_Liquidacion", "Descripcion", _objLiquidacion.LIQUIDACION.Id_Tipo_Liquidacion);
                ViewBag.Empleados_Id_Empleado = new SelectList(db.empleados.Include(e => e.persona).ToList(), "Id_Empleado", "persona.Nombre", _objLiquidacion.LIQUIDACION.Empleados_Id_Empleado);
                return View(_objLiquidacion);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }

        }


        // GET: Liquidacion/Delete/5
        public ActionResult Delete(int? idLiquidacion, int? idEmpleado)
        {
            if (idLiquidacion == null || idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.liquidacion lIQUIDACION = db.liquidacions.FirstOrDefault(i => i.Id_Liquidacion == idLiquidacion && i.Empleados_Id_Empleado == idEmpleado);
            if (lIQUIDACION == null)
            {
                return HttpNotFound();
            }
            return View(lIQUIDACION);
        }

        // POST: Liquidacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idLiquidacion, int? idEmpleado)
        {
            try
            {
                DB.Models.liquidacion lIQUIDACION = db.liquidacions.FirstOrDefault(i => i.Id_Liquidacion == idLiquidacion && i.Empleados_Id_Empleado == idEmpleado);
                DB.Models.detalles_liquidacion Detalles = db.detalles_liquidacion.FirstOrDefault(i => i.Liquidacion_Id_Liquidacion == idLiquidacion && i.Liquidacion_Empleados_Id_Empleado == idEmpleado);
                db.detalles_liquidacion.Remove(Detalles);
                db.SaveChanges();
                db.liquidacions.Remove(lIQUIDACION);
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
