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
    public class ProvinciaController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Provincia
        public ActionResult Index()
        {
            return View(db.provincias.ToList());
        }

        // GET: Provincia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            provincia pROVINCIA = db.provincias.Find(id);
            if (pROVINCIA == null)
            {
                return HttpNotFound();
            }
            return View(pROVINCIA);
        }

        // GET: Provincia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provincia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProvincia,Descripcion")] provincia pROVINCIA)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.provincias.Add(pROVINCIA);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(pROVINCIA);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Provincia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            provincia pROVINCIA = db.provincias.Find(id);
            if (pROVINCIA == null)
            {
                return HttpNotFound();
            }
            return View(pROVINCIA);
        }

        // POST: Provincia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProvincia,Descripcion")] provincia pROVINCIA)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pROVINCIA).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pROVINCIA);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Provincia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            provincia pROVINCIA = db.provincias.Find(id);
            if (pROVINCIA == null)
            {
                return HttpNotFound();
            }
            return View(pROVINCIA);
        }

        // POST: Provincia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                provincia pROVINCIA = db.provincias.Find(id);
                db.provincias.Remove(pROVINCIA);
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
