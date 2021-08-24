using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class TreatmentReport
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Tr_Id { get; set; }
        [ForeignKey("PatientMedicalReport")]
        public string New_P_Id { get; set; }
        [ForeignKey("Doctor")]
        public int D_Id { get; set; }
        [ForeignKey("Patient")]
        public int P_Id { get; set; }
        [Required(ErrorMessage ="this field is required")]

        public string Disease { get; set; }
        [Required(ErrorMessage = "this field is required")]

        public string Prescription { get; set; }
        [ForeignKey("HospitalDepartment")]
        public int Dep_Id { get; set; }
        [Required(ErrorMessage ="Date time is required")]
        [DataType(DataType.Date)]
        [Display(Name ="Date Time")]
        public DateTime Date_Time { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual PatientMedicalReport PatientMedicalReport { get; set; }
        public virtual HospitalDepartment HospitalDepartment { get; set; }

        public virtual Patient Patient { get; set; }


    }
}