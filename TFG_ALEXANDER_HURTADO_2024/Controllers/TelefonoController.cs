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
    public class TelefonoController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Telefono
        public ActionResult Index()
        {
            var tELEFONOes = db.telefonoes.Include(t => t.persona);
            return View(tELEFONOes.ToList());
        }

        // GET: Telefono/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            telefono tELEFONO = db.telefonoes.Find(id);
            if (tELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tELEFONO);
        }

        // GET: Telefono/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre");
            return View();
        }

        // POST: Telefono/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTelefono,Activo,Cedula")] telefono tELEFONO)
        {
            if (ModelState.IsValid)
            {
                db.telefonoes.Add(tELEFONO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", tELEFONO.Persona_Cedula);
            return View(tELEFONO);
        }

        // GET: Telefono/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            telefono tELEFONO = db.telefonoes.Find(id);
            if (tELEFONO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", tELEFONO.Persona_Cedula);
            return View(tELEFONO);
        }

        // POST: Telefono/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTelefono,Activo,Cedula")] telefono tELEFONO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tELEFONO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.personas, "Cedula", "Nombre", tELEFONO.Persona_Cedula);
            return View(tELEFONO);
        }

        // GET: Telefono/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            telefono tELEFONO = db.telefonoes.Find(id);
            if (tELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tELEFONO);
        }

        // POST: Telefono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            telefono tELEFONO = db.telefonoes.Find(id);
            db.telefonoes.Remove(tELEFONO);
            db.SaveChanges();
            return RedirectToAction("Index");
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
