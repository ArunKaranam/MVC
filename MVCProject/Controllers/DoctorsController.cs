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
    public class DoctorsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        [OutputCache(Duration =20,Location =System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult DoctorDashboard()
        {
            string name = User.Identity.Name;
            int d_id = db.doctors.Where(x => x.UserName == name).FirstOrDefault().D_Id;
            Appointment remaind = db.appointments.Where(x => x.D_Id == d_id && x.Status == true && x.Date >= DateTime.Today).FirstOrDefault();
            if (remaind != null)
            {
                if (remaind.Time >= DateTime.Now)
                    TempData["remainder"] = "your appointment is at " + remaind.Time;
                



            }



            return View();
        }



        public ActionResult sheduleFrom()
        {
            string username = User.Identity.Name;

            int id = db.doctors.Where(x => x.UserName == username).FirstOrDefault().D_Id;

            var list = db.hospitalAdminSchedules.Where(x => x.D_Id == id).ToList();

            if(list.Count()>0)
                return View(list);
            else
                return View("_NoData");


        }


        [HttpGet]
        public ActionResult EditDoctor()

        {
            string username = User.Identity.Name;

            int id = db.doctors.Where(x => x.UserName == username).FirstOrDefault().D_Id;


            Doctor doctor = db.doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EditDoctor")]
        public ActionResult _EditDcotor()
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;

                int id = db.doctors.Where(x => x.UserName == username).FirstOrDefault().D_Id;
                Doctor d = db.doctors.Find(id);

                UpdateModel(d);
                db.SaveChanges();
                return RedirectToAction("DoctorDashboard", "Doctors");
            }
            return View("EditDoctor");
        }



        [HttpGet]
        public ActionResult DoctorShedule()
        {
            ViewBag.D_Id = new SelectList(db.doctors.Where(d => d.UserName == User.Identity.Name), "D_Id", "FirstName");
            ViewBag.S_Id = new SelectList(db.doctorAndSpecialists, "S_Id", "Specialists");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.Approved == true), "H_Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DoctorShedule")]
        public ActionResult _DoctorShedule()
        {
            if (ModelState.IsValid)
            {
                DoctorUpdate doctorUpdate = new DoctorUpdate();
                TryUpdateModel(doctorUpdate);
                db.doctorUpdates.Add(doctorUpdate);
                db.SaveChanges();
                return RedirectToAction("DoctorDashboard", "Doctors");
            }

            ViewBag.D_Id = new SelectList(db.doctors.Where(d => d.UserName == User.Identity.Name), "D_Id", "FirstName");
            ViewBag.S_Id = new SelectList(db.doctorAndSpecialists, "S_Id", "Specialists");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.Approved == true), "H_Id", "Name");
            return View();
        }
        public ActionResult ReShedule()
        {
            int id = db.doctors.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().D_Id;

            var doctorUpdates = db.doctorUpdates.Where(d => d.D_Id == id).Include(d => d.Doctor).Include(d => d.DoctorAndSpecialist).Include(d => d.HospitalAdmin);
            return View(doctorUpdates.ToList());
        }
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorUpdate doctorUpdate = db.doctorUpdates.Find(id);
            if (doctorUpdate == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.UserName == User.Identity.Name), "D_Id", "FirstName", doctorUpdate.D_Id);
            ViewBag.S_Id = new SelectList(db.doctorAndSpecialists, "S_Id", "Specialists", doctorUpdate.S_Id);
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.Approved == true), "H_Id", "Name", doctorUpdate.H_Id);
            return View(doctorUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit1")]
        public ActionResult Edit1([Bind(Include = "Up_Id,D_Id,H_Id,Date,S_Time,E_Time,S_Id")] DoctorUpdate doctorUpdate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ReShedule", "Doctors");
            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.UserName == User.Identity.Name), "D_Id", "FirstName", doctorUpdate.D_Id);
            ViewBag.S_Id = new SelectList(db.doctorAndSpecialists, "S_Id", "Specialists", doctorUpdate.S_Id);
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.Approved == true), "H_Id", "Name", doctorUpdate.H_Id);
            return View(doctorUpdate);
        }
        [HttpGet]
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorUpdate doctorUpdate = db.doctorUpdates.Find(id);
            if (doctorUpdate == null)
            {
                return HttpNotFound();
            }
            return View(doctorUpdate);
        }

        // POST: DoctorUpdates/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorUpdate doctorUpdate = db.doctorUpdates.Find(id);
            db.doctorUpdates.Remove(doctorUpdate);
            db.SaveChanges();
            return RedirectToAction("ReShedule", "Doctors");
        }


        public ActionResult NewAppointment()
        {
            string username = User.Identity.Name;

            int id = db.doctors.Where(x => x.UserName == username).FirstOrDefault().D_Id;

            var list = db.appointments.Where(x => x.D_Id == id && x.Status == false).ToList();


            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");





        }
        [HttpGet]
        public ActionResult NewAppointmentAprovel(int? id)
        {
            int d_id = db.appointments.Where(x => x.App_Id == id).FirstOrDefault().D_Id;
            int h_id = db.appointments.Where(x => x.App_Id == id).FirstOrDefault().H_Id;
            int p_id = db.appointments.Where(x => x.App_Id == id).FirstOrDefault().P_Id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.H_Id == h_id), "H_Id", "Name");
            ViewBag.P_Id = new SelectList(db.patients.Where(x => x.P_Id == p_id), "P_Id", "FirstName");
            return View(appointment);

        }
        [HttpPost]
        [ActionName("NewAppointmentAprovel")]
        [ValidateAntiForgeryToken]
        public ActionResult _NewAppointmentAprovel(int? id)
        {
            int d_id = db.appointments.Where(x => x.App_Id == id).FirstOrDefault().D_Id;
            int h_id = db.appointments.Where(x => x.App_Id == id).FirstOrDefault().H_Id;
            int p_id = db.appointments.Where(x => x.App_Id == id).FirstOrDefault().P_Id;


            Appointment a = db.appointments.Find(id);
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.H_Id == h_id), "H_Id", "Name");
            ViewBag.P_Id = new SelectList(db.patients.Where(x => x.P_Id == p_id), "P_Id", "FirstName");
            if (ModelState.IsValid)
            {
               

                UpdateModel(a);
                db.SaveChanges();
                if (a.Status == true)
                {

                    Autopatient auto = new Autopatient();
                    auto.autogenerateid(a);
                }

               return RedirectToAction("NewAppointment", "Doctors");

            }




            return View(a);


           


        }

        public ActionResult ApprovedAppointment()
        {
            string username = User.Identity.Name;

            int id = db.doctors.Where(x => x.UserName == username).FirstOrDefault().D_Id;

            var list = db.appointments.Where(x => x.D_Id == id && x.Status == true).ToList();


            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");





        }


        public ActionResult medicalRecord()
        {
            string username = User.Identity.Name;

            int id = db.doctors.Where(x => x.UserName == username).FirstOrDefault().D_Id;

            var list = db.patientMedicalReports.Where(x => x.D_Id == id).ToList();

            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");

        
        
        
        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            int d_id = db.patientMedicalReports.Where(x => x.New_P_Id == id).FirstOrDefault().D_Id;
            int p_id = db.patientMedicalReports.Where(x => x.New_P_Id == id).FirstOrDefault().P_Id;




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientMedicalReport patientMedicalReport = db.patientMedicalReports.Find(id);
            if (patientMedicalReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x=>x.D_Id==d_id), "D_Id", "FirstName");
            ViewBag.P_Id = new SelectList(db.patients.Where(x=>x.P_Id==p_id), "P_Id", "FirstName");
            return View(patientMedicalReport);





        }
        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]

        public ActionResult _Update(string id)
        {
            int d_id = db.patientMedicalReports.Where(x => x.New_P_Id == id).FirstOrDefault().D_Id;
            int p_id = db.patientMedicalReports.Where(x => x.New_P_Id == id).FirstOrDefault().P_Id;
            PatientMedicalReport p = db.patientMedicalReports.Find(id);
            if (ModelState.IsValid)
            {
                
                UpdateModel(p);
                db.SaveChanges();
                return RedirectToAction("medicalRecord");
            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.P_Id = new SelectList(db.patients.Where(x => x.P_Id == p_id), "P_Id", "FirstName");

            return View(p);






        }
        [HttpGet]
        public ActionResult ReportforPatient(string id)
        {
            int d_id = db.treatmentReports.Where(x => x.New_P_Id == id).FirstOrDefault().D_Id;
            int p_id = db.treatmentReports.Where(x => x.New_P_Id == id).FirstOrDefault().P_Id;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreatmentReport treatmentReport = db.treatmentReports.Where(x=>x.New_P_Id==id).FirstOrDefault();
            if (treatmentReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x=>x.D_Id==d_id), "D_Id", "FirstName");
            ViewBag.Dep_Id = new SelectList(db.hospitalDepartments, "Dep_Id", "Department_Name");
            ViewBag.P_Id = new SelectList(db.patients.Where(x=>x.P_Id==p_id), "P_Id", "FirstName");
            ViewBag.New_P_Id = new SelectList(db.patientMedicalReports.Where(x=>x.New_P_Id==id), "New_P_Id","New_P_Id");
            return View(treatmentReport);




        }

        [HttpPost]
        [ActionName("ReportforPatient")]
        [ValidateAntiForgeryToken]
        public ActionResult _ReportforPatient(string id)
        {

            int d_id = db.treatmentReports.Where(x => x.New_P_Id == id).FirstOrDefault().D_Id;
            int p_id = db.treatmentReports.Where(x => x.New_P_Id == id).FirstOrDefault().P_Id;

            TreatmentReport t = db.treatmentReports.Where(x => x.New_P_Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                UpdateModel(t);
                db.SaveChanges();
                return RedirectToAction("medicalRecord");





            }
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.Dep_Id = new SelectList(db.hospitalDepartments, "Dep_Id", "Department_Name");
            ViewBag.P_Id = new SelectList(db.patients.Where(x => x.P_Id == p_id), "P_Id", "FirstName");
            ViewBag.New_P_Id = new SelectList(db.patientMedicalReports.Where(x => x.New_P_Id == id), "New_P_Id", "Diagnosis");
            return View(t);

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
