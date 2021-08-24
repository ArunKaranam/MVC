using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int D_Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "4 to 15 are only allowed")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "4 to 15 are only allowed")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Date of Birth")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "please select gender ")]
        public string Gender { get; set; }
        [Required(ErrorMessage = " please provide contact number ")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]

        public long ContactNumber { get; set; }
        [Required(ErrorMessage ="please provide email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "User name is required")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "7 to 15 are only allowed")]
        [Remote("IsUserExists", "Account", ErrorMessage = "User Name already in use")]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "password is required")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "7 to 15 are only allowed")]

        public string PassWord { get; set; }
        [Display(Name = "Security question")]

        [Required(ErrorMessage = "This field is required")]

        public string S_Que { get; set; }
        [Display(Name = "Security answer")]

        [Required(ErrorMessage ="This field is required")]


        
        public string S_ANSWER { get; set; }
        [Required(ErrorMessage = "select, userrole is required")]

        public string RoleName { get; set; }


        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PatientMedicalReport> PatientMedicalReports { get; set; }

        public virtual ICollection<HospitalAdminSchedule> HospitalAdminSchedules { get; set; }
        public virtual ICollection<DoctorUpdate> DoctorUpdates { get; set; }

        public virtual ICollection<TreatmentReport> TreatmentReports { get; set; }










    }
}