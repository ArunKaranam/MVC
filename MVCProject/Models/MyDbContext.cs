using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext():base("MedicalTourism"){}

        public DbSet<User> users { get; set; }

       

        public DbSet<HospitalAdmin> hospitalAdmins { get; set; }

        public DbSet<HospitalAdminSchedule> hospitalAdminSchedules { get; set; }

        public DbSet<Doctor> doctors { get; set; }

        public DbSet<DoctorUpdate> doctorUpdates { get; set; }

        public DbSet<DoctorAndSpecialist> doctorAndSpecialists { get; set; }

        public DbSet<Patient> patients { get; set; }
        public DbSet<Appointment> appointments { get; set; }

        public DbSet<TreatmentReport> treatmentReports { get; set; }

        public DbSet<PatientMedicalReport> patientMedicalReports { get; set; }

        public DbSet<HospitalDepartment> hospitalDepartments { get; set; }

        public DbSet<Help> helps { get; set; }

        public DbSet<FeedBack> feedBacks { get; set; }




    }
}