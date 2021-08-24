namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        App_Id = c.Int(nullable: false, identity: true),
                        P_Id = c.Int(nullable: false),
                        H_Id = c.Int(nullable: false),
                        D_Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.App_Id)
                .ForeignKey("dbo.Doctors", t => t.D_Id, cascadeDelete: true)
                .ForeignKey("dbo.HospitalAdmins", t => t.H_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.P_Id, cascadeDelete: true)
                .Index(t => t.P_Id)
                .Index(t => t.H_Id)
                .Index(t => t.D_Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        D_Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 15),
                        Dob = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 15),
                        PassWord = c.String(nullable: false, maxLength: 15),
                        S_Que = c.String(nullable: false),
                        S_ANSWER = c.String(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.D_Id);
            
            CreateTable(
                "dbo.DoctorUpdates",
                c => new
                    {
                        Up_Id = c.Int(nullable: false, identity: true),
                        D_Id = c.Int(nullable: false),
                        H_Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        S_Time = c.DateTime(nullable: false),
                        E_Time = c.DateTime(nullable: false),
                        S_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Up_Id)
                .ForeignKey("dbo.Doctors", t => t.D_Id, cascadeDelete: true)
                .ForeignKey("dbo.DoctorAndSpecialists", t => t.S_Id, cascadeDelete: true)
                .ForeignKey("dbo.HospitalAdmins", t => t.H_Id, cascadeDelete: true)
                .Index(t => t.D_Id)
                .Index(t => t.H_Id)
                .Index(t => t.S_Id);
            
            CreateTable(
                "dbo.DoctorAndSpecialists",
                c => new
                    {
                        S_Id = c.Int(nullable: false, identity: true),
                        Specialists = c.String(),
                    })
                .PrimaryKey(t => t.S_Id);
            
            CreateTable(
                "dbo.HospitalAdmins",
                c => new
                    {
                        H_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 250),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Certification = c.String(maxLength: 250),
                        SuccessfulOperation = c.String(maxLength: 250),
                        Achievements = c.String(maxLength: 250),
                        Speciality = c.String(),
                        Facilities = c.String(maxLength: 250),
                        Treatment = c.String(maxLength: 250),
                        UserName = c.String(nullable: false, maxLength: 15),
                        PassWord = c.String(nullable: false, maxLength: 15),
                        S_Que = c.String(nullable: false),
                        S_ANSWER = c.String(nullable: false),
                        RoleName = c.String(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.H_Id);
            
            CreateTable(
                "dbo.HospitalAdminSchedules",
                c => new
                    {
                        Sch_Id = c.Int(nullable: false, identity: true),
                        D_Id = c.Int(nullable: false),
                        H_Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        S_Time = c.DateTime(nullable: false),
                        E_Time = c.DateTime(nullable: false),
                        Purpose = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Sch_Id)
                .ForeignKey("dbo.Doctors", t => t.D_Id, cascadeDelete: true)
                .ForeignKey("dbo.HospitalAdmins", t => t.H_Id, cascadeDelete: true)
                .Index(t => t.D_Id)
                .Index(t => t.H_Id);
            
            CreateTable(
                "dbo.PatientMedicalReports",
                c => new
                    {
                        New_P_Id = c.String(nullable: false, maxLength: 128),
                        P_Id = c.Int(nullable: false),
                        D_Id = c.Int(nullable: false),
                        Diagnosis = c.String(nullable: false),
                        Treatment = c.String(nullable: false),
                        Medicine = c.String(nullable: false),
                        Revisit = c.String(),
                    })
                .PrimaryKey(t => t.New_P_Id)
                .ForeignKey("dbo.Doctors", t => t.D_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.P_Id, cascadeDelete: true)
                .Index(t => t.P_Id)
                .Index(t => t.D_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        P_Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 15),
                        Dob = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 15),
                        PassWord = c.String(nullable: false, maxLength: 15),
                        S_Que = c.String(nullable: false),
                        S_ANSWER = c.String(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.P_Id);
            
            CreateTable(
                "dbo.TreatmentReports",
                c => new
                    {
                        Tr_Id = c.Int(nullable: false, identity: true),
                        New_P_Id = c.String(maxLength: 128),
                        D_Id = c.Int(nullable: false),
                        P_Id = c.Int(nullable: false),
                        Disease = c.String(nullable: false),
                        Prescription = c.String(nullable: false),
                        Dep_Id = c.Int(nullable: false),
                        Date_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Tr_Id)
                .ForeignKey("dbo.Doctors", t => t.D_Id, cascadeDelete: true)
                .ForeignKey("dbo.HospitalDepartments", t => t.Dep_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.P_Id, cascadeDelete: true)
                .ForeignKey("dbo.PatientMedicalReports", t => t.New_P_Id)
                .Index(t => t.New_P_Id)
                .Index(t => t.D_Id)
                .Index(t => t.P_Id)
                .Index(t => t.Dep_Id);
            
            CreateTable(
                "dbo.HospitalDepartments",
                c => new
                    {
                        Dep_Id = c.Int(nullable: false, identity: true),
                        Department_Name = c.String(),
                    })
                .PrimaryKey(t => t.Dep_Id);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        F_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        answer = c.String(),
                        Review = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.F_Id);
            
            CreateTable(
                "dbo.Helps",
                c => new
                    {
                        I_Id = c.Int(nullable: false, identity: true),
                        Issue = c.String(),
                        Description = c.String(maxLength: 250),
                        TicketDate = c.DateTime(nullable: false),
                        Solution = c.String(),
                        U_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.I_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        PassWord = c.String(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "P_Id", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "H_Id", "dbo.HospitalAdmins");
            DropForeignKey("dbo.Appointments", "D_Id", "dbo.Doctors");
            DropForeignKey("dbo.PatientMedicalReports", "P_Id", "dbo.Patients");
            DropForeignKey("dbo.TreatmentReports", "New_P_Id", "dbo.PatientMedicalReports");
            DropForeignKey("dbo.TreatmentReports", "P_Id", "dbo.Patients");
            DropForeignKey("dbo.TreatmentReports", "Dep_Id", "dbo.HospitalDepartments");
            DropForeignKey("dbo.TreatmentReports", "D_Id", "dbo.Doctors");
            DropForeignKey("dbo.PatientMedicalReports", "D_Id", "dbo.Doctors");
            DropForeignKey("dbo.DoctorUpdates", "H_Id", "dbo.HospitalAdmins");
            DropForeignKey("dbo.HospitalAdminSchedules", "H_Id", "dbo.HospitalAdmins");
            DropForeignKey("dbo.HospitalAdminSchedules", "D_Id", "dbo.Doctors");
            DropForeignKey("dbo.DoctorUpdates", "S_Id", "dbo.DoctorAndSpecialists");
            DropForeignKey("dbo.DoctorUpdates", "D_Id", "dbo.Doctors");
            DropIndex("dbo.TreatmentReports", new[] { "Dep_Id" });
            DropIndex("dbo.TreatmentReports", new[] { "P_Id" });
            DropIndex("dbo.TreatmentReports", new[] { "D_Id" });
            DropIndex("dbo.TreatmentReports", new[] { "New_P_Id" });
            DropIndex("dbo.PatientMedicalReports", new[] { "D_Id" });
            DropIndex("dbo.PatientMedicalReports", new[] { "P_Id" });
            DropIndex("dbo.HospitalAdminSchedules", new[] { "H_Id" });
            DropIndex("dbo.HospitalAdminSchedules", new[] { "D_Id" });
            DropIndex("dbo.DoctorUpdates", new[] { "S_Id" });
            DropIndex("dbo.DoctorUpdates", new[] { "H_Id" });
            DropIndex("dbo.DoctorUpdates", new[] { "D_Id" });
            DropIndex("dbo.Appointments", new[] { "D_Id" });
            DropIndex("dbo.Appointments", new[] { "H_Id" });
            DropIndex("dbo.Appointments", new[] { "P_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Helps");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.HospitalDepartments");
            DropTable("dbo.TreatmentReports");
            DropTable("dbo.Patients");
            DropTable("dbo.PatientMedicalReports");
            DropTable("dbo.HospitalAdminSchedules");
            DropTable("dbo.HospitalAdmins");
            DropTable("dbo.DoctorAndSpecialists");
            DropTable("dbo.DoctorUpdates");
            DropTable("dbo.Doctors");
            DropTable("dbo.Appointments");
        }
    }
}
