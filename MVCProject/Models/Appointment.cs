using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Appointment
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int App_Id { get; set; }
        [ForeignKey("Patient")]
        [Display(Name ="Patient Name")]
        public int P_Id { get; set; }
        [ForeignKey("HospitalAdmin")]
        [Display(Name = "Hospital Name")]
        public int H_Id { get; set; }
        [ForeignKey("Doctor")]
        [Display(Name = "Doctor Name")]
        public int D_Id { get; set; }
        [Required(ErrorMessage = "Please enter the date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter the  time")]

        [DataType(DataType.Time)]
        [Display(Name = " Time")]
        public DateTime Time { get; set; }
       /* [Required(ErrorMessage = "Please enter the end time")]

        [DataType(DataType.Time)]

        [Display(Name ="End Time")]
        public DateTime E_Time { get; set; }*/
        public bool Status { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual HospitalAdmin HospitalAdmin { get; set; }




    }
}