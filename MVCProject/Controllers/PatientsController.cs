using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using Rotativa;
using PagedList;
using PagedList.Mvc;
namespace MVCProject.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Patients
        [OutputCache(Duration = 20, Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult PatientDashBoard()
        {
            string name = User.Identity.Name;
            int p_id = db.patients.Where(x => x.UserName == name).FirstOrDefault().P_Id;
            Appointment remaind = db.appointments.Where(x => x.P_Id == p_id && x.Status == true && x.Date >=DateTime.Today).FirstOrDefault();
            if (remaind !=null)
            {
                if (remaind.Time >= DateTime.Now)
                    TempData["remainder"] = "your appointment is at " + remaind.Time;
                else
                    TempData["remainder"] = "please provide the Feedback ";



            }

            return View();
        
        }
        public ActionResult Index()
        {
            return View(db.patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Download()
        {
            string username = User.Identity.Name;
            int id = db.patients.Where(x => x.UserName == username).FirstOrDefault().P_Id;


            var report = db.treatmentReports.Where(x => x.P_Id == id).FirstOrDefault();
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }
        public ActionResult value(int? id)
        {
            var report = db.treatmentReports.Where(x => x.P_Id == id).FirstOrDefault();
            return View(report);
        }

        public ActionResult ConvertToPDF(int? id)
        {
            //var printpdf = new ActionAsPdf("Download");
            var printpdf = new Rotativa.ActionAsPdf("value", new { id = id });
            return printpdf;
        }

        // GET: Patients/Create

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "P_Id,FirstName,LastName,Dob,Gender,ContactNumber,Email,UserName,PassWord,S_Que,S_ANSWER,RoleName")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.patients.Find(id);
            db.patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult search(string searchBy, string search,int? Page)
        {
            if (searchBy == "Doctor")
            {

                return View(db.doctorUpdates.Where(x => x.Doctor.FirstName.StartsWith(search) || search == null).ToList().ToPagedList(Page ?? 1, 5));

            }
            else if (searchBy == "City")
            {
                return View(db.doctorUpdates.Where(x => x.HospitalAdmin.City.StartsWith(search) || search == null).ToList().ToPagedList(Page ?? 1, 5));

            }
            else 
            {

                return View(db.doctorUpdates.Where(x => x.DoctorAndSpecialist.Specialists.StartsWith(search) || search == null).ToList().ToPagedList(Page ?? 1, 5));
            }

            





        }

        public ActionResult GetDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sin = db.doctorUpdates.Find(id);
            if (sin == null)
            {
                return HttpNotFound();
            }
            return View(sin);


        }
        [HttpGet]
        public ActionResult Appointment(int? id)
        {
            int d_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().D_Id;
            int h_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().H_Id;

            string username = User.Identity.Name;

            int p_id = db.patients.Where(x => x.UserName == username).FirstOrDefault().P_Id;
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.H_Id == h_id), "H_Id", "Name");
            ViewBag.P_Id = new SelectList(db.patients.Where(x => x.P_Id == p_id), "P_Id", "FirstName");
            return View();







        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Appointment")]
        public ActionResult _Appointment(int? id)
        {
            int d_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().D_Id;
            int h_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().H_Id;

            string username = User.Identity.Name;

            int p_id = db.patients.Where(x => x.UserName == username).FirstOrDefault().P_Id;
            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.H_Id == h_id), "H_Id", "Name");
            ViewBag.P_Id = new SelectList(db.patients.Where(x => x.P_Id == p_id), "P_Id", "FirstName");


            Appointment p = new Appointment();
            if (ModelState.IsValid)
            {
                TryUpdateModel(p);
                var taken = db.appointments.Where(x => x.D_Id == p.D_Id).ToList();
                //int count = taken.LastOrDefault().App_Id;
                
                if (taken.Count() == 0)
                {
                    db.appointments.Add(p);
                    db.SaveChanges();
                    TempData["appointment"] = "your Appointment booked";
                    //return RedirectToAction("PatientDashBoard", "Patients");
                    return RedirectToAction("Appointment", "Patients");


                }
                else

               {
                    int count = taken.Max(x => x.App_Id);
                    if (((db.appointments.Where(x => x.App_Id == count).FirstOrDefault().Time).AddMinutes(5)) <= p.Time)
                    {
                        db.appointments.Add(p);
                        db.SaveChanges();
                        TempData["appointment"] = "your Appointment booked";
                        //return RedirectToAction("PatientDashBoard", "Patients");
                        return RedirectToAction("Appointment", "Patients");

                        // return RedirectToAction("PatientDashBoard", "Patients");
                        

                    }
                    else
                    {
                        TempData["noslot"] = "No Slot Available at this  "+ (db.appointments.Where(x => x.App_Id == count).FirstOrDefault().Time).AddMinutes(5)+" taken after this time";
                        return RedirectToAction("Appointment", "Patients");
                    }
                }

            }

            return View("Appointment");

        }

        public ActionResult Listing(int? id)
        {
           
           
              
                var list = db.hospitalAdmins.Where(x => x.H_Id == id).FirstOrDefault();

          
           
            return View(list);
           


        }
        public ActionResult printing(int? id)
        {
            var list = db.hospitalAdmins.Where(x => x.H_Id == id).FirstOrDefault();
            return View(list);
        
        }

        public ActionResult ConvertToPDFS(int? id)
        {
            
            //var printpdf = new ActionAsPdf(pan.ToString());
            var printpdf= new Rotativa.ActionAsPdf("printing", new { id = id });
            return printpdf;
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
