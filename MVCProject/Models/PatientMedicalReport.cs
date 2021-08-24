using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class PatientMedicalReport
    {
        [Key]
        public string New_P_Id { get; set; }
        [ForeignKey("Patient")]

        public int P_Id { get; set; }
        [ForeignKey("Doctor")]
        public int D_Id { get; set; }
        [Required(ErrorMessage ="This field need to filled")]
        public string Diagnosis { get; set; }
        [Required(ErrorMessage = "This field need to filled")]

        public string Treatment { get; set; }
        [Required(ErrorMessage = "This field need to filled")]
        public string  Medicine { get; set; }
        public string Revisit { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual ICollection<TreatmentReport> TreatmentReports { get; set; }



    }
}