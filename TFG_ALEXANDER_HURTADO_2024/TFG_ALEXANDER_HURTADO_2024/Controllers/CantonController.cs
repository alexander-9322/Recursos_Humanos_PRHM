using DB.Models;
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
    public class CantonController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Canton
        public ActionResult Index()
        {
            var cANTONs = db.cantons.Include(c => c.provincia);
            return View(cANTONs.ToList());
        }

        // GET: Canton/Details/5
        public ActionResult Details(int? idCanton, int? idProvincia)
        {
            if (idCanton == null || idProvincia == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.canton cANTON = db.cantons.Find(idCanton, idProvincia);
            if (cANTON == null)
            {
                return HttpNotFound();
            }
            return View(cANTON);
        }

        // GET: Canton/Create
        public ActionResult Create()
        {
            ViewBag.IdProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion");
            return View();
        }

        // POST: Canton/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCanton,Descripcion,Provincia_idProvincia")] DB.Models.canton cANTON)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.cantons.Add(cANTON);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion", cANTON.Provincia_idProvincia);
                return View(cANTON);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Canton/Edit/5
        public ActionResult Edit(int? idCanton, int? idProvincia)
        {
            if (idCanton == null || idProvincia == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DB.Models.canton cANTON = db.cantons.Find(idCanton, idProvincia);
            if (cANTON == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion", cANTON.Provincia_idProvincia);
            return View(cANTON);
        }

        // POST: Canton/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(DB.Models.canton cANTON)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (RHEntities db2 = new RHEntities())
                    {

                        db2.Database.ExecuteSqlCommand("SP_EditCanton @IdCanton, @IdProvincia, @Descripcion",
                                                                   new SqlParameter("@IdCanton", cANTON.idCanton),
                                                                   new SqlParameter("@IdProvincia", cANTON.Provincia_idProvincia),
                                                                   new SqlParameter("@Descripcion", cANTON.Descripcion));
                    }

                    return RedirectToAction("Index");
                }

                ViewBag.IdProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion", cANTON.Provincia_idProvincia);
                return View(cANTON);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
           
        }



        // GET: Canton/Delete/5
        public ActionResult Delete(int? idCanton, int? idProvincia)
        {
            if (idCanton == null || idProvincia == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DB.Models.canton cANTON = db.cantons.Find(idCanton, idProvincia);
            if (cANTON == null)
            {
                return HttpNotFound();
            }
            return View(cANTON);
        }

        // POST: Canton/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idCanton, int idProvincia)
        {
            try
            {
                try
                {
                    db.Database.ExecuteSqlCommand("SP_EliminarCanton @IdCanton",
                                                                  new SqlParameter("@IdCanton", idCanton));
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Examinar la excepción interna para obtener más detalles
                    Exception innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        // Puedes imprimir o registrar los detalles de la excepción interna
                        Console.WriteLine(innerException.Message);
                        innerException = innerException.InnerException;
                    }

                    // Puedes personalizar el manejo de errores aquí según tus necesidades
                    return View("Error"); // Puedes redirigir a una vista de error personalizada
                }
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
