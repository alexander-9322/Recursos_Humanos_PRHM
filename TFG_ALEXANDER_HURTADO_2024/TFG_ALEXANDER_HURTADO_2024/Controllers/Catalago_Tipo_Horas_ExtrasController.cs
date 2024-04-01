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
    public class Catalago_Tipo_Horas_ExtrasController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Catalago_Tipo_Horas_Extras
        public ActionResult Index()
        {
            return View(db.catalago_tipo_horas_extras.ToList());
        }

        // GET: Catalago_Tipo_Horas_Extras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_tipo_horas_extras cATALOGO_TIPO_HORAS_EXTRAS = db.catalago_tipo_horas_extras.Find(id);
            if (cATALOGO_TIPO_HORAS_EXTRAS == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_TIPO_HORAS_EXTRAS);
        }

        // GET: Catalago_Tipo_Horas_Extras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalago_Tipo_Horas_Extras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Hora_Extra,Descripcion,Porcentaje")] catalago_tipo_horas_extras cATALOGO_TIPO_HORAS_EXTRAS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.catalago_tipo_horas_extras.Add(cATALOGO_TIPO_HORAS_EXTRAS);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cATALOGO_TIPO_HORAS_EXTRAS);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
           
        }

        // GET: Catalago_Tipo_Horas_Extras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_tipo_horas_extras cATALOGO_TIPO_HORAS_EXTRAS = db.catalago_tipo_horas_extras.Find(id);
            if (cATALOGO_TIPO_HORAS_EXTRAS == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_TIPO_HORAS_EXTRAS);
        }

        // POST: Catalago_Tipo_Horas_Extras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Hora_Extra,Descripcion,Porcentaje")] catalago_tipo_horas_extras cATALOGO_TIPO_HORAS_EXTRAS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cATALOGO_TIPO_HORAS_EXTRAS).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cATALOGO_TIPO_HORAS_EXTRAS);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalago_Tipo_Horas_Extras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_tipo_horas_extras cATALOGO_TIPO_HORAS_EXTRAS = db.catalago_tipo_horas_extras.Find(id);
            if (cATALOGO_TIPO_HORAS_EXTRAS == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_TIPO_HORAS_EXTRAS);
        }

        // POST: Catalago_Tipo_Horas_Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                catalago_tipo_horas_extras cATALOGO_TIPO_HORAS_EXTRAS = db.catalago_tipo_horas_extras.Find(id);
                db.catalago_tipo_horas_extras.Remove(cATALOGO_TIPO_HORAS_EXTRAS);
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
