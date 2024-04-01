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
    public class Catalago_DeduccionesController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Catalago_Deducciones
        public ActionResult Index()
        {
            return View(db.catalago_deducciones.ToList());
        }

        // GET: Catalago_Deducciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_deducciones cATALAGO_DEDUCCIONES = db.catalago_deducciones.Find(id);
            if (cATALAGO_DEDUCCIONES == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_DEDUCCIONES);
        }

        // GET: Catalago_Deducciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalago_Deducciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCatalago_Deducciones,Descripcion,Monto")] catalago_deducciones cATALAGO_DEDUCCIONES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.catalago_deducciones.Add(cATALAGO_DEDUCCIONES);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cATALAGO_DEDUCCIONES);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalago_Deducciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_deducciones cATALAGO_DEDUCCIONES = db.catalago_deducciones.Find(id);
            if (cATALAGO_DEDUCCIONES == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_DEDUCCIONES);
        }

        // POST: Catalago_Deducciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCatalago_Deducciones,Descripcion,Monto")] catalago_deducciones cATALAGO_DEDUCCIONES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cATALAGO_DEDUCCIONES).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cATALAGO_DEDUCCIONES);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalago_Deducciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_deducciones cATALAGO_DEDUCCIONES = db.catalago_deducciones.Find(id);
            if (cATALAGO_DEDUCCIONES == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_DEDUCCIONES);
        }

        // POST: Catalago_Deducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                catalago_deducciones cATALAGO_DEDUCCIONES = db.catalago_deducciones.Find(id);
                db.catalago_deducciones.Remove(cATALAGO_DEDUCCIONES);
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
