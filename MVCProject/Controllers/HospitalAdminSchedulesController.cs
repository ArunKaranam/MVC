using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using PagedList;
using PagedList.Mvc;

namespace MVCProject.Controllers
{
    [Authorize]
    public class HospitalAdminSchedulesController : Controller
    {
        private MyDbContext db = new MyDbContext();


        public ActionResult HospitalAdminDashboard()
        {

            return View();
        }

        // GET: HospitalAdminSchedules
        public ActionResult GetDoctors(int? Page)
        {
            string username = User.Identity.Name;
            int id = db.hospitalAdmins.Where(x => x.UserName == username).FirstOrDefault().H_Id;

            var mylist = db.doctorUpdates.Where(x => x.H_Id == id).ToList().ToPagedList(Page ?? 1,4);
            if (mylist.Count > 0)
                return View(mylist);
            else
                return View("_NoData");
        }
        public ActionResult IsShedule(int? id)
        {
            int d_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().D_Id;
            int h_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().H_Id;

            var list = db.hospitalAdminSchedules.Where(x => x.H_Id == h_id && x.D_Id == d_id).ToList();

            if (list.Count() > 0)
                return View(list);
            else
                return View("_NoData");

        }

        public ActionResult AdminShedule()
        {
            string username = User.Identity.Name;
            int id = db.hospitalAdmins.Where(x => x.UserName == username).FirstOrDefault().H_Id;

            var hospitalAdminSchedules = db.hospitalAdminSchedules.Where(x=>x.H_Id==id).Include(h => h.Doctor).Include(h => h.HospitalAdmin);
                
            return View(hospitalAdminSchedules.ToList());


        }
       
        // GET: HospitalAdminSchedules/Create
       [HttpGet]

        public ActionResult Shedule(int? id)
        {

            int d_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().D_Id;
            int h_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().H_Id;

            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.H_Id == h_id), "H_Id", "Name");
            return View();







        }

        



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Shedule")]

        public ActionResult _Shedule(int? id)
        {
            int d_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().D_Id;
            int h_id = db.doctorUpdates.Where(x => x.Up_Id == id).FirstOrDefault().H_Id;


            if (ModelState.IsValid)
            {
                HospitalAdminSchedule h = new HospitalAdminSchedule();
                TryUpdateModel(h);
                db.hospitalAdminSchedules.Add(h);

                db.SaveChanges();
                return RedirectToAction("GetDoctors");
            }

            ViewBag.D_Id = new SelectList(db.doctors.Where(x => x.D_Id == d_id), "D_Id", "FirstName");
            ViewBag.H_Id = new SelectList(db.hospitalAdmins.Where(x => x.H_Id == h_id), "H_Id", "Name");
            return View();



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
