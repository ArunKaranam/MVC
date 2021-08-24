using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MVCProject.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Admin

        public ActionResult AdminDashBoard()
        {
            var man = db.hospitalAdmins.Where(x => x.Approved == false);
            if (man != null)
                TempData["newhospital"] = "new hospital";

           
            
            return View();
        }
        public ActionResult Patientslist(int? Page)
        {
            var list = db.patients.ToList().ToPagedList(Page ?? 1,5);
            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");




        }
        public ActionResult Doctorslist(int? Page)
        {
            var list = db.doctors.ToList().ToPagedList(Page ?? 1, 5);
            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");




        }
        public ActionResult Appointmentlist(int? Page)
        {
            var list = db.appointments.ToList().ToPagedList(Page ?? 1, 5);
            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");



        }




        public ActionResult HospitalApprovel(int? Page)
        {

            var list = db.hospitalAdmins.ToList().ToPagedList(Page ?? 1, 10);
            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");


           

        }
        public ActionResult Update(int id)
        {
            HospitalAdmin a = db.hospitalAdmins.Find(id);

            return View(a);
        }
        [HttpPost]
        [ActionName("Update")]
        public ActionResult _Update(int id)
        {
            HospitalAdmin a = db.hospitalAdmins.Find(id);


            UpdateModel(a);
            db.SaveChanges();
            TempData["Message"] = "Hospital Succesfully Approved/pending";

            return RedirectToAction("HospitalApprovel","Admin");
        }
        public ActionResult Reject(int id)
        {
            HospitalAdmin a = db.hospitalAdmins.Find(id);

            return View(a);
        
        }
        [HttpPost]
        [ActionName("Reject")]
        public ActionResult _Reject(int id)
        {
            HospitalAdmin a = db.hospitalAdmins.Find(id);


            db.hospitalAdmins.Remove(a);
            db.SaveChanges();
            TempData["Message"] = "Hospital Succesfully Removed";

            return RedirectToAction("HospitalApprovel", "Admin");


        }
    }
}