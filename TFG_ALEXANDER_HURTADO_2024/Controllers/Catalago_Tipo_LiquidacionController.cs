using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.Models;

namespace DB.Models.Controllers
{
    public class Catalago_Tipo_LiquidacionController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Catalago_Tipo_Liquidacion
        public ActionResult Index()
        {
            return View(db.catalago_tipo_liquidacion.ToList());
        }

        // GET: Catalago_Tipo_Liquidacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_tipo_liquidacion cATALAGO_TIPO_LIQUIDACION = db.catalago_tipo_liquidacion.Find(id);
            if (cATALAGO_TIPO_LIQUIDACION == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_TIPO_LIQUIDACION);
        }

        // GET: Catalago_Tipo_Liquidacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalago_Tipo_Liquidacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tipo_Liquidacion,Descripcion")] catalago_tipo_liquidacion cATALAGO_TIPO_LIQUIDACION)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.catalago_tipo_liquidacion.Add(cATALAGO_TIPO_LIQUIDACION);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cATALAGO_TIPO_LIQUIDACION);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalago_Tipo_Liquidacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_tipo_liquidacion cATALAGO_TIPO_LIQUIDACION = db.catalago_tipo_liquidacion.Find(id);
            if (cATALAGO_TIPO_LIQUIDACION == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_TIPO_LIQUIDACION);
        }

        // POST: Catalago_Tipo_Liquidacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tipo_Liquidacion,Descripcion")] catalago_tipo_liquidacion cATALAGO_TIPO_LIQUIDACION)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cATALAGO_TIPO_LIQUIDACION).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cATALAGO_TIPO_LIQUIDACION);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalago_Tipo_Liquidacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_tipo_liquidacion cATALAGO_TIPO_LIQUIDACION = db.catalago_tipo_liquidacion.Find(id);
            if (cATALAGO_TIPO_LIQUIDACION == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_TIPO_LIQUIDACION);
        }

        // POST: Catalago_Tipo_Liquidacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                catalago_tipo_liquidacion cATALAGO_TIPO_LIQUIDACION = db.catalago_tipo_liquidacion.Find(id);
                db.catalago_tipo_liquidacion.Remove(cATALAGO_TIPO_LIQUIDACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
