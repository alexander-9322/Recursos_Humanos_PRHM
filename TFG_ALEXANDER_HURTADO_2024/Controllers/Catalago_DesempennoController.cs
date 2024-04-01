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
    public class Catalago_DesempennoController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Catalago_Desempenno
        public ActionResult Index()
        {
            return View(db.catalago_desempeño.ToList());
        }

        // GET: Catalago_Desempenno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_desempeño cATALOGO_DESEMPENNO = db.catalago_desempeño.Find(id);
            if (cATALOGO_DESEMPENNO == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_DESEMPENNO);
        }

        // GET: Catalago_Desempenno/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalago_Desempenno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Desempeño,Descripcion")] catalago_desempeño cATALOGO_DESEMPENNO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.catalago_desempeño.Add(cATALOGO_DESEMPENNO);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cATALOGO_DESEMPENNO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
           
        }

        // GET: Catalago_Desempenno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_desempeño cATALOGO_DESEMPENNO = db.catalago_desempeño.Find(id);
            if (cATALOGO_DESEMPENNO == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_DESEMPENNO);
        }

        // POST: Catalago_Desempenno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Desempeño,Descripcion")] catalago_desempeño cATALOGO_DESEMPENNO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cATALOGO_DESEMPENNO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cATALOGO_DESEMPENNO);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalago_Desempenno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalago_desempeño cATALOGO_DESEMPENNO = db.catalago_desempeño.Find(id);
            if (cATALOGO_DESEMPENNO == null)
            {
                return HttpNotFound();
            }
            return View(cATALOGO_DESEMPENNO);
        }

        // POST: Catalago_Desempenno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                catalago_desempeño cATALOGO_DESEMPENNO = db.catalago_desempeño.Find(id);
                db.catalago_desempeño.Remove(cATALOGO_DESEMPENNO);
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
