using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TriCourier.Models;

namespace TriCourier.Controllers
{
    [Authorize]
    public class Delivery_AgentController : Controller
    {
        private Dunzo db = new Dunzo();

        // GET: Delivery_Agent
        public ActionResult Index()
        {
            var delivery_Agent = db.Delivery_Agent.Include(d => d.Vehicle);
            return View(delivery_Agent.ToList());
        }

        // GET: Delivery_Agent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_Agent delivery_Agent = db.Delivery_Agent.Find(id);
            if (delivery_Agent == null)
            {
                return HttpNotFound();
            }
            return View(delivery_Agent);
        }

        // GET: Delivery_Agent/Create
        public ActionResult Create()
        {
            ViewBag.Vehicle_Id = new SelectList(db.Vehicles, "Id", "Make");
            return View();
        }

        // POST: Delivery_Agent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Phone_No,Email_Id,Status,Vehicle_Id")] Delivery_Agent delivery_Agent)
        {
            if (ModelState.IsValid)
            {
                db.Delivery_Agent.Add(delivery_Agent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vehicle_Id = new SelectList(db.Vehicles, "Id", "Make", delivery_Agent.Vehicle_Id);
            return View(delivery_Agent);
        }

        // GET: Delivery_Agent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_Agent delivery_Agent = db.Delivery_Agent.Find(id);
            if (delivery_Agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vehicle_Id = new SelectList(db.Vehicles, "Id", "Make", delivery_Agent.Vehicle_Id);
            return View(delivery_Agent);
        }

        // POST: Delivery_Agent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Phone_No,Email_Id,Status,Vehicle_Id")] Delivery_Agent delivery_Agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery_Agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vehicle_Id = new SelectList(db.Vehicles, "Id", "Make", delivery_Agent.Vehicle_Id);
            return View(delivery_Agent);
        }

        // GET: Delivery_Agent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_Agent delivery_Agent = db.Delivery_Agent.Find(id);
            if (delivery_Agent == null)
            {
                return HttpNotFound();
            }
            return View(delivery_Agent);
        }

        // POST: Delivery_Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delivery_Agent delivery_Agent = db.Delivery_Agent.Find(id);
            db.Delivery_Agent.Remove(delivery_Agent);
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
