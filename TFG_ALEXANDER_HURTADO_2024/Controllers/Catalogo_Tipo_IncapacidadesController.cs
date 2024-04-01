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
    public class Catalogo_Tipo_IncapacidadesController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Catalogo_Tipo_Incapacidades
        public ActionResult Index()
        {
            return View(db.catalogo_tipo_incapacidades.ToList());
        }

        // GET: Catalogo_Tipo_Incapacidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalogo_tipo_incapacidades cATALAGO_TIPO_INCAPACIDADES = db.catalogo_tipo_incapacidades.Find(id);
            if (cATALAGO_TIPO_INCAPACIDADES == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_TIPO_INCAPACIDADES);
        }

        // GET: Catalogo_Tipo_Incapacidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogo_Tipo_Incapacidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(catalogo_tipo_incapacidades cATALAGO_TIPO_INCAPACIDADES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.catalogo_tipo_incapacidades.Add(cATALAGO_TIPO_INCAPACIDADES);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cATALAGO_TIPO_INCAPACIDADES);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo Crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalogo_Tipo_Incapacidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalogo_tipo_incapacidades cATALAGO_TIPO_INCAPACIDADES = db.catalogo_tipo_incapacidades.Find(id);
            if (cATALAGO_TIPO_INCAPACIDADES == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_TIPO_INCAPACIDADES);
        }

        // POST: Catalogo_Tipo_Incapacidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(catalogo_tipo_incapacidades cATALAGO_TIPO_INCAPACIDADES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cATALAGO_TIPO_INCAPACIDADES).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cATALAGO_TIPO_INCAPACIDADES);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo Modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalogo_Tipo_Incapacidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalogo_tipo_incapacidades cATALAGO_TIPO_INCAPACIDADES = db.catalogo_tipo_incapacidades.Find(id);
            if (cATALAGO_TIPO_INCAPACIDADES == null)
            {
                return HttpNotFound();
            }
            return View(cATALAGO_TIPO_INCAPACIDADES);
        }

        // POST: Catalogo_Tipo_Incapacidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                catalogo_tipo_incapacidades cATALAGO_TIPO_INCAPACIDADES = db.catalogo_tipo_incapacidades.Find(id);
                db.catalogo_tipo_incapacidades.Remove(cATALAGO_TIPO_INCAPACIDADES);
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
