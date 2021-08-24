namespace MVCProject.Migrations
{
    using MVCProject.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCProject.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCProject.Models.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.users.AddOrUpdate(x => x.Id,
             new User() { Id = 1, UserName = "superadmin21", PassWord = "superadmin@21", RoleName = "admin" }


             );


            context.doctorAndSpecialists.AddOrUpdate(x => x.S_Id,
                new DoctorAndSpecialist() { S_Id = 1, Specialists = "Cardiologist" },
                new DoctorAndSpecialist() { S_Id = 2, Specialists = "Diabetologist" },
                 new DoctorAndSpecialist() { S_Id = 3, Specialists = "Endocrinologist" },
                 new DoctorAndSpecialist() { S_Id = 4, Specialists = "ENT Specialist" },
                   new DoctorAndSpecialist() { S_Id = 5, Specialists = "Gastroenterologist" },
                    new DoctorAndSpecialist() { S_Id = 6, Specialists = "General Physician" },
                      new DoctorAndSpecialist() { S_Id = 7, Specialists = "General Surgeon" },
                     new DoctorAndSpecialist() { S_Id = 8, Specialists = "Gynecologist" }





                );

            context.hospitalDepartments.AddOrUpdate(x => x.Dep_Id,
              new HospitalDepartment() { Dep_Id = 1, Department_Name = "Anesthetics" },
              new HospitalDepartment() { Dep_Id = 2, Department_Name = "Cardiology" },
              new HospitalDepartment() { Dep_Id = 3, Department_Name = "Ear, nose and throat (ENT)" },
              new HospitalDepartment() { Dep_Id = 4, Department_Name = "Elderly services department" },
              new HospitalDepartment() { Dep_Id = 5, Department_Name = "Gastroenterology" },
              new HospitalDepartment() { Dep_Id = 6, Department_Name = "General Surgery" },
              new HospitalDepartment() { Dep_Id = 7, Department_Name = "Gynecology" },
              new HospitalDepartment() { Dep_Id = 8, Department_Name = "Hematology" },
              new HospitalDepartment() { Dep_Id = 9, Department_Name = "cardiology" },
              new HospitalDepartment() { Dep_Id = 10, Department_Name = "Neurology" },
              new HospitalDepartment() { Dep_Id = 11, Department_Name = "Nutrition and dietetics" },
              new HospitalDepartment() { Dep_Id = 12, Department_Name = "Obstetrics and gynecology units" },
              new HospitalDepartment() { Dep_Id = 13, Department_Name = "Oncology" },
              new HospitalDepartment() { Dep_Id = 14, Department_Name = "Orthopedics" },
              new HospitalDepartment() { Dep_Id = 15, Department_Name = "Physiotherapy" },
              new HospitalDepartment() { Dep_Id = 16, Department_Name = "Renal Unit" },
              new HospitalDepartment() { Dep_Id = 17, Department_Name = "Urology" });
            //new HospitalDepartment() { Dep_Id = 18, Department_Name = "Anesthetics" }


        }
    }
}
