using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.appointments.Include(a => a.Doctor).Include(a => a.HospitalAdmin).Include(a => a.Patient);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins, "H_Id", "Name");
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "App_Id,P_Id,H_Id,D_Id,Date,Time,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", appointment.D_Id);
            ViewBag.H_Id = new SelectList(db.hospitalAdmins, "H_Id", "Name", appointment.H_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", appointment.P_Id);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", appointment.D_Id);
            ViewBag.H_Id = new SelectList(db.hospitalAdmins, "H_Id", "Name", appointment.H_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", appointment.P_Id);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "App_Id,P_Id,H_Id,D_Id,Date,Time,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", appointment.D_Id);
            ViewBag.H_Id = new SelectList(db.hospitalAdmins, "H_Id", "Name", appointment.H_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", appointment.P_Id);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.appointments.Find(id);
            db.appointments.Remove(appointment);
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
