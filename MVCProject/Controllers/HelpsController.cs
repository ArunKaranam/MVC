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
    public class HelpsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Helps
        public ActionResult Index()
        {
            return View(db.helps.ToList());
        }

        // GET: Helps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Help help = db.helps.Find(id);
            if (help == null)
            {
                return HttpNotFound();
            }
            return View(help);
        }

        public ActionResult helptouser()
        {

            string username = User.Identity.Name;
            int id = db.users.Where(x => x.UserName == username).FirstOrDefault().Id;

            var h = db.helps.Where(x => x.U_Id == id).ToList();
            if (h == null)
            {
                return HttpNotFound();
            }
            return View(h);


        }






        // GET: Helps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Helps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult _Create()
        {
           
            Help h = new Help();
            if (ModelState.IsValid)
            {

               
                TryUpdateModel(h);

                string username = User.Identity.Name;
                h.U_Id = db.users.Where(x => x.UserName ==username).FirstOrDefault().Id;                

                db.helps.Add(h);

                db.SaveChanges();

                string roll = db.users.Where(x => x.UserName == username).FirstOrDefault().RoleName;

                if (roll == "admin")
                {

                    return RedirectToAction("AdminDashBoard", "Admin");
                }
                else if (roll == "patient")
                {
                    return RedirectToAction("PatientDashBoard", "Patients");
                }
                else if (roll == "doctor")
                {
                    return RedirectToAction("DoctorDashboard", "Doctors");
                }
                else if (roll == "hospitaladmin")
                {
                    return RedirectToAction("HospitalAdminDashboard", "HospitalAdminSchedules");
                }

                
            }

            return View(h);
        }

        // GET: Helps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Help help = db.helps.Find(id);
            if (help == null)
            {
                return HttpNotFound();
            }
            return View(help);
        }

        // POST: Helps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "I_Id,Issue,Description,TicketDate,Solution,U_Id")] Help help)
        {
            if (ModelState.IsValid)
            {
                db.Entry(help).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(help);
        }

        // GET: Helps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Help help = db.helps.Find(id);
            if (help == null)
            {
                return HttpNotFound();
            }
            return View(help);
        }

        // POST: Helps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Help help = db.helps.Find(id);
            db.helps.Remove(help);
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
