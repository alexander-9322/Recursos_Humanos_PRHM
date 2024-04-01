using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.Models;

namespace TFG_ALEXANDER_HURTADO_2024.Controllers
{
    public class DistritoController : Controller
    {
        private RHEntities db = new RHEntities();

        // GET: Distrito
        public ActionResult Index()
        {
            var dISTRITOes = db.distritoes.Include(d => d.canton).Include(c => c.canton.provincia);
            
            return View(dISTRITOes.ToList());
        }

        // GET: Distrito/Details/5
        public ActionResult Details(int? id, int? idCanton, int? idProvincia)
        {
            if (id == null || idCanton == null || idProvincia == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            distrito dISTRITO = db.distritoes.FirstOrDefault(d => d.idDistrito == id && d.Canton_idCanton == idCanton && d.Canton_Provincia_idProvincia == idProvincia);
            if (dISTRITO == null)
            {
                return HttpNotFound();
            }
            return View(dISTRITO);
        }

        // GET: Distrito/Create
        public ActionResult Create()
        {
            ViewBag.Canton_idCanton = new SelectList(db.cantons, "IdCanton", "Descripcion");
            ViewBag.Canton_Provincia_idProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion");
            return View();
        }

        // POST: Distrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDistrito,Descripcion,Canton_idCanton,Canton_Provincia_idProvincia")] distrito dISTRITO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.distritoes.Add(dISTRITO);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Canton_idCanton = new SelectList(db.cantons, "IdCanton", "Descripcion", dISTRITO.Canton_idCanton);
                ViewBag.Canton_Provincia_idProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion", dISTRITO.Canton_Provincia_idProvincia);
                return View(dISTRITO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Distrito/Edit/5
        public ActionResult Edit(int? id, int? idCanton, int? idProvincia)
        {
            if (id == null || idCanton == null || idProvincia == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            distrito dISTRITO = db.distritoes.FirstOrDefault(d => d.idDistrito == id && d.Canton_idCanton == idCanton && d.Canton_Provincia_idProvincia == idProvincia);
            if (dISTRITO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Canton_idCanton = new SelectList(db.cantons, "IdCanton", "Descripcion", dISTRITO.Canton_idCanton);
            ViewBag.Canton_Provincia_idProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion", dISTRITO.Canton_Provincia_idProvincia);
            return View(dISTRITO);
        }

        // POST: Distrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDistrito,Descripcion,Canton_idCanton,Canton_Provincia_idProvincia")] distrito dISTRITO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(dISTRITO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Canton_idCanton = new SelectList(db.cantons, "IdCanton", "Descripcion", dISTRITO.Canton_idCanton);
                ViewBag.Canton_Provincia_idProvincia = new SelectList(db.provincias, "IdProvincia", "Descripcion", dISTRITO.Canton_Provincia_idProvincia);
                return View(dISTRITO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
           
        }

        // GET: Distrito/Delete/5
        public ActionResult Delete(int? id, int? idCanton, int? idProvincia)
        {
            if (id == null || idCanton == null || idProvincia == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            distrito dISTRITO = db.distritoes.FirstOrDefault(d => d.idDistrito == id && d.Canton_idCanton == idCanton && d.Canton_Provincia_idProvincia == idProvincia);
            if (dISTRITO == null)
            {
                return HttpNotFound();
            }
            return View(dISTRITO);
        }

        // POST: Distrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? idCanton, int? idProvincia)
        {
            try
            {
                distrito dISTRITO = db.distritoes.FirstOrDefault(d => d.idDistrito == id && d.Canton_idCanton == idCanton && d.Canton_Provincia_idProvincia == idProvincia);
                db.distritoes.Remove(dISTRITO);
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
