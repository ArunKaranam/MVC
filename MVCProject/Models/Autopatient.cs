using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
   
    public  class Autopatient
    {

        MyDbContext db = new MyDbContext();

         
       
        public void autogenerateid(Appointment a)

        {
            int num;
            int count = db.patientMedicalReports.ToList().Count();
            if (count == 0)
                num = 0;
            else
                num = count;


                int first = a.H_Id;
            string name = db.hospitalAdmins.Where(x => x.H_Id == first).FirstOrDefault().Name.Substring(0, 3);

            string pad = (++num).ToString().PadLeft(4, '0');

            string id = String.Concat(name,pad);

            PatientMedicalReport p = new PatientMedicalReport();
            p.New_P_Id = id;
            p.P_Id = a.P_Id;
            p.D_Id = a.D_Id;
            p.Medicine = "Need to fill";
            p.Diagnosis = "Need to fill";
            p.Treatment = "Need to fill";
            p.Revisit = " Need to fill";

            db.patientMedicalReports.Add(p);
            db.SaveChanges();
            TreatmentReport t = new TreatmentReport();
            t.New_P_Id = id;
            t.P_Id = a.P_Id;
            t.D_Id = a.D_Id;
            t.Disease = "Null";
            t.Prescription = "Null";
            t.Date_Time = DateTime.Now;
            t.Dep_Id = 5;
            db.treatmentReports.Add(t);
            db.SaveChanges();





        }










    }
}