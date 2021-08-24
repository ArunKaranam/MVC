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
    public class PatientMedicalReportsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: PatientMedicalReports
        public ActionResult Index()
        {
            var patientMedicalReports = db.patientMedicalReports.Include(p => p.Doctor).Include(p => p.Patient);
            return View(patientMedicalReports.ToList());
        }

        // GET: PatientMedicalReports/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicalReport patientMedicalReport = db.patientMedicalReports.Find(id);
            if (patientMedicalReport == null)
            {
                return HttpNotFound();
            }
            return View(patientMedicalReport);
        }

        // GET: PatientMedicalReports/Create
        public ActionResult Create()
        {
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName");
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName");
            return View();
        }

        // POST: PatientMedicalReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "New_P_Id,P_Id,D_Id,Diagnosis,Treatment,Medicine,Revisit")] PatientMedicalReport patientMedicalReport)
        {
            if (ModelState.IsValid)
            {
                db.patientMedicalReports.Add(patientMedicalReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", patientMedicalReport.D_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", patientMedicalReport.P_Id);
            return View(patientMedicalReport);
        }

        // GET: PatientMedicalReports/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicalReport patientMedicalReport = db.patientMedicalReports.Find(id);
            if (patientMedicalReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", patientMedicalReport.D_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", patientMedicalReport.P_Id);
            return View(patientMedicalReport);
        }

        // POST: PatientMedicalReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "New_P_Id,P_Id,D_Id,Diagnosis,Treatment,Medicine,Revisit")] PatientMedicalReport patientMedicalReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientMedicalReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", patientMedicalReport.D_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", patientMedicalReport.P_Id);
            return View(patientMedicalReport);
        }

        // GET: PatientMedicalReports/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicalReport patientMedicalReport = db.patientMedicalReports.Find(id);
            if (patientMedicalReport == null)
            {
                return HttpNotFound();
            }
            return View(patientMedicalReport);
        }

        // POST: PatientMedicalReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PatientMedicalReport patientMedicalReport = db.patientMedicalReports.Find(id);
            db.patientMedicalReports.Remove(patientMedicalReport);
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
