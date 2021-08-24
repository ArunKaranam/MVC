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
    public class TreatmentReportsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TreatmentReports
        public ActionResult Index()
        {
            var treatmentReports = db.treatmentReports.Include(t => t.Doctor).Include(t => t.HospitalDepartment).Include(t => t.Patient).Include(t => t.PatientMedicalReport);
            return View(treatmentReports.ToList());
        }

        // GET: TreatmentReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.treatmentReports.Find(id);
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            return View(treatmentReport);
        }

        // GET: TreatmentReports/Create
        public ActionResult Create()
        {
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName");
            ViewBag.Dep_Id = new SelectList(db.hospitalDepartments, "Dep_Id", "Department_Name");
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName");
            ViewBag.New_P_Id = new SelectList(db.patientMedicalReports, "New_P_Id", "Diagnosis");
            return View();
        }

        // POST: TreatmentReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tr_Id,New_P_Id,D_Id,P_Id,Disease,Prescription,Dep_Id,Date_Time")] TreatmentReport treatmentReport)
        {
            if (ModelState.IsValid)
            {
                db.treatmentReports.Add(treatmentReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", treatmentReport.D_Id);
            ViewBag.Dep_Id = new SelectList(db.hospitalDepartments, "Dep_Id", "Department_Name", treatmentReport.Dep_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", treatmentReport.P_Id);
            ViewBag.New_P_Id = new SelectList(db.patientMedicalReports, "New_P_Id", "Diagnosis", treatmentReport.New_P_Id);
            return View(treatmentReport);
        }

        // GET: TreatmentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.treatmentReports.Find(id);
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", treatmentReport.D_Id);
            ViewBag.Dep_Id = new SelectList(db.hospitalDepartments, "Dep_Id", "Department_Name", treatmentReport.Dep_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", treatmentReport.P_Id);
            ViewBag.New_P_Id = new SelectList(db.patientMedicalReports, "New_P_Id", "Diagnosis", treatmentReport.New_P_Id);
            return View(treatmentReport);
        }

        // POST: TreatmentReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tr_Id,New_P_Id,D_Id,P_Id,Disease,Prescription,Dep_Id,Date_Time")] TreatmentReport treatmentReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatmentReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_Id = new SelectList(db.doctors, "D_Id", "FirstName", treatmentReport.D_Id);
            ViewBag.Dep_Id = new SelectList(db.hospitalDepartments, "Dep_Id", "Department_Name", treatmentReport.Dep_Id);
            ViewBag.P_Id = new SelectList(db.patients, "P_Id", "FirstName", treatmentReport.P_Id);
            ViewBag.New_P_Id = new SelectList(db.patientMedicalReports, "New_P_Id", "Diagnosis", treatmentReport.New_P_Id);
            return View(treatmentReport);
        }

        // GET: TreatmentReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.treatmentReports.Find(id);
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            return View(treatmentReport);
        }

        // POST: TreatmentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreatmentReport treatmentReport = db.treatmentReports.Find(id);
            db.treatmentReports.Remove(treatmentReport);
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
