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
    public class Catalogo_Control_permisos_ColaboradorController : Controller
    {
        private  RHEntities db = new  RHEntities();

        // GET: Catalogo_Control_permisos_Colaborador
        public ActionResult Index()
        {
            return View(db.catalogo_control_permisos_colaborador.ToList());
        }

        // GET: Catalogo_Control_permisos_Colaborador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalogo_control_permisos_colaborador catalogo_Control_permisos_Colaborador = db.catalogo_control_permisos_colaborador.Find(id);
            if (catalogo_Control_permisos_Colaborador == null)
            {
                return HttpNotFound();
            }
            return View(catalogo_Control_permisos_Colaborador);
        }

        // GET: Catalogo_Control_permisos_Colaborador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogo_Control_permisos_Colaborador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCatalogo_Control_permisos_Colaborador,Descripcion")] catalogo_control_permisos_colaborador catalogo_Control_permisos_Colaborador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.catalogo_control_permisos_colaborador.Add(catalogo_Control_permisos_Colaborador);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(catalogo_Control_permisos_Colaborador);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo crear";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalogo_Control_permisos_Colaborador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalogo_control_permisos_colaborador catalogo_Control_permisos_Colaborador = db.catalogo_control_permisos_colaborador.Find(id);
            if (catalogo_Control_permisos_Colaborador == null)
            {
                return HttpNotFound();
            }
            return View(catalogo_Control_permisos_Colaborador);
        }

        // POST: Catalogo_Control_permisos_Colaborador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCatalogo_Control_permisos_Colaborador,Descripcion")] catalogo_control_permisos_colaborador catalogo_Control_permisos_Colaborador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(catalogo_Control_permisos_Colaborador).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(catalogo_Control_permisos_Colaborador);
            }
            catch (Exception)
            {
                Session["Error"] = "No se pudo modificar";
                return RedirectToAction("Index");
            }
            
        }

        // GET: Catalogo_Control_permisos_Colaborador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catalogo_control_permisos_colaborador catalogo_Control_permisos_Colaborador = db.catalogo_control_permisos_colaborador.Find(id);
            if (catalogo_Control_permisos_Colaborador == null)
            {
                return HttpNotFound();
            }
            return View(catalogo_Control_permisos_Colaborador);
        }

        // POST: Catalogo_Control_permisos_Colaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                catalogo_control_permisos_colaborador catalogo_Control_permisos_Colaborador = db.catalogo_control_permisos_colaborador.Find(id);
                db.catalogo_control_permisos_colaborador.Remove(catalogo_Control_permisos_Colaborador);
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
