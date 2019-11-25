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
    public class BookingsController : Controller
    {
        private Dunzo db = new Dunzo();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Category).Include(b => b.Customer).Include(b => b.Delivery_Agent1);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details()
        {
            var id = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Booking> booking = db.Bookings.Where(t => t.Customer.Email_Id == id).ToList();
            if (booking.Count() == 0)
            {
                return RedirectToAction("Create","Bookings");
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Customer_ID = new SelectList(db.Customers, "Id", "Name");
            ViewBag.Delivery_Agent = new SelectList(db.Delivery_Agent, "Id", "Phone_No");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,From_addr,To_Addr,Booking_Date,Weight,Category_Id,Delivery_Agent,Customer_ID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", booking.Category_Id);
            ViewBag.Customer_ID = new SelectList(db.Customers, "Id", "Name", booking.Customer_ID);
            ViewBag.Delivery_Agent = new SelectList(db.Delivery_Agent, "Id", "Phone_No", booking.Customer_ID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", booking.Category_Id);
            ViewBag.Customer_ID = new SelectList(db.Customers, "Id", "Name", booking.Customer_ID);
            ViewBag.Customer_ID = new SelectList(db.Delivery_Agent, "Id", "Phone_No", booking.Customer_ID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,From_addr,To_Addr,Booking_Date,Weight,Category_Id,Delivery_Agent,Customer_ID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", booking.Category_Id);
            ViewBag.Customer_ID = new SelectList(db.Customers, "Id", "Name", booking.Customer_ID);
            ViewBag.Customer_ID = new SelectList(db.Delivery_Agent, "Id", "Phone_No", booking.Customer_ID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
