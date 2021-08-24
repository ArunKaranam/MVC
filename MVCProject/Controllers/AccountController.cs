using MVCProject.Models;
using MVCProject.NewClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
      private  MyDbContext db = new MyDbContext();
        // GET: Account
       
        [HttpGet]
        public ActionResult Login()

        {
           /* TempData["Message"] = "Your Details added Succesfilly";*/

            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult _Login(User user)
        {
            using (var db = new MyDbContext())
            {
                bool isvalid = db.users.Any(x => x.UserName == user.UserName && x.PassWord == user.PassWord && x.RoleName==user.RoleName);


                if (isvalid)
                {

                    FormsAuthentication.SetAuthCookie(user.UserName, false);


                    // string current_user = User.Identity.Name;
                    // string current= Session["Username"].ToString();
                    var kill = db.users.FirstOrDefault(x => x.UserName == user.UserName);


                    string roll = kill.RoleName;


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
                        bool isgiven = db.hospitalAdmins.Any(x => x.UserName == user.UserName && x.PassWord == user.PassWord && x.RoleName == user.RoleName && x.Approved == true);
                        if (isgiven)
                            return RedirectToAction("HospitalAdminDashboard", "HospitalAdminSchedules");
                        else
                            //ModelState.AddModelError("", "Admin permissions is needed to login");
                            TempData["details"] = "Need Admin permissions to login";



                    }




                }
                else

                {
                    ModelState.AddModelError("", "Invalid user,please check the Cerdentials ");
                    TempData["details"] = "please the provide the details";
                }


               

            }

            return View();


        }
        [HttpGet]
        public ActionResult SignUp() 
        {
            return View();
        
        }
        [HttpGet]
        public ActionResult PatientSignUp()
        {

            return View();
        
        
        }
        [HttpPost]
        [ActionName("PatientSignUp")]
        [ValidateAntiForgeryToken]
        public ActionResult _PatientSignUp()
        {
            if (ModelState.IsValid)
            {
                Patient p = new Patient();
                TryUpdateModel(p);
                db.patients.Add(p);
                db.SaveChanges();

                User u = new User();
                u.UserName = p.UserName;
                u.PassWord = p.PassWord;
                u.RoleName = p.RoleName;
                db.users.Add(u);
                db.SaveChanges();

                TempData["Message"] = "Your Details added Succesfully";  

               

            }
            

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult DoctorSignUp()
        {
           

           
            return View();
        }
        [HttpPost]
        [ActionName("DoctorSignUp")]
        [ValidateAntiForgeryToken]
        public ActionResult _DoctorSignUp()
        {



            if (ModelState.IsValid)
            {
                Doctor d = new Doctor();
                TryUpdateModel(d);
                db.doctors.Add(d);
                db.SaveChanges();

                User u = new User();
                u.UserName = d.UserName;
                u.PassWord = d.PassWord;
                u.RoleName = d.RoleName;
                db.users.Add(u);
                db.SaveChanges();

                TempData["Message"] = "Your Details added Succesfully";



            }


            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult HospitalAdminSignUp()
        {
            return View();
        }
        [HttpPost]
        [ActionName("HospitalAdminSignUp")]
        [ValidateAntiForgeryToken]
        public ActionResult _HospitalAdminSignUp()
        {

            if (ModelState.IsValid)
            {
                HospitalAdmin h = new HospitalAdmin();
                TryUpdateModel(h);
                db.hospitalAdmins.Add(h);
                db.SaveChanges();

                User u = new User();
                u.UserName = h.UserName;
                u.PassWord = h.PassWord;
                u.RoleName = h.RoleName;
                db.users.Add(u);
                db.SaveChanges();

                TempData["Message"] = "Your Details added Succesfully";



            }

            return RedirectToAction("Login");

        }


        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        
        }
        public JsonResult IsUserExists(string UserName)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!db.users.Any(x => x.UserName == UserName), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult RetriveUserName()
        {

            return View();
        }

        [HttpPost]
        [ActionName("RetriveUserName")]
        [ValidateAntiForgeryToken]
        public ActionResult _RetriveUserName(GetUserName u)
        {
            bool patient = db.patients.Any(x => x.S_Que ==u.S_Question && x.S_ANSWER ==u.S_Answer && x.ContactNumber==u.Phone);
            bool doctor=  db.doctors.Any(x => x.S_Que ==u.S_Question && x.S_ANSWER ==u.S_Answer && x.ContactNumber == u.Phone);
           
           
                if (patient)
                    TempData["user"] = "User Name is : "+db.patients.Where(x => x.ContactNumber == u.Phone).FirstOrDefault().UserName;





                else if (doctor)
                    TempData["user"] = "User Name is : " + db.doctors.Where(x => x.ContactNumber == u.Phone).FirstOrDefault().UserName;

                else
                    TempData["user"] = "No User  Name is not Found";



                return RedirectToAction("RetriveUserName");
           
            
        }
        [HttpGet]
        public ActionResult PassWordRest()
        {
            return View();
        }
        [HttpPost]
        [ActionName("PassWordRest")]
        [ValidateAntiForgeryToken]
        public ActionResult _PassWordRest(PasswordRest p)
        {
            bool patient = db.patients.Any(x => x.S_Que == p.S_Question && x.S_ANSWER == p.S_Answer && x.UserName == p.UserName);
            bool doctor = db.doctors.Any(x => x.S_Que == p.S_Question && x.S_ANSWER == p.S_Answer && x.UserName == p.UserName);

            if (patient)
            {
                User u = db.users.Where(x => x.UserName == p.UserName).FirstOrDefault();
                u.PassWord = p.password;
                db.SaveChanges();

                Patient pat = db.patients.Where(x => x.UserName == p.UserName).FirstOrDefault();
                pat.PassWord = p.password;
                db.SaveChanges();
                TempData["rest"] = "Your PassWord updated Succeesfully";

            }
            else if (doctor)
            {
                User u = db.users.Where(x => x.UserName == p.UserName).FirstOrDefault();
                u.PassWord = p.password;
                db.SaveChanges();
                Doctor doc = db.doctors.Where(x => x.UserName == p.UserName).FirstOrDefault();
                doc.PassWord = p.password;
                db.SaveChanges();
                TempData["rest"] = "Your PassWord updated Succeesfully";


            }
            else
                TempData["rest"] = "Unable to updated ,since user name or security answer may be incorrect";




            return RedirectToAction("PassWordRest");
        
        }

        public ActionResult board()
        {
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