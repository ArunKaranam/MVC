using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class DoctorUpdate
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Up_Id  { get; set; }
        [ForeignKey("Doctor")]
        [Display(Name ="Doctor Name")]
        public int D_Id { get; set; }
        [ForeignKey("HospitalAdmin")]
        [Display(Name = "Hospital Name")]
        public int H_Id { get; set; }
        [Required(ErrorMessage ="data is requried here")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
         [Required(ErrorMessage = "Please enter the start time")]

        [DataType(DataType.Time)]

        [Display(Name ="Start Time")]


        public DateTime S_Time { get; set; }
        [Required(ErrorMessage = "Please enter the end time")]

        [DataType(DataType.Time)]

        [Display(Name = "End Time")]

        public DateTime E_Time { get; set; }
        [ForeignKey("DoctorAndSpecialist")]
        [Display(Name = "Select Speciality")]
        public int S_Id { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual HospitalAdmin HospitalAdmin { get; set; }

       // public virtual DoctorAndSpecialist DoctorAndSpecialist { get; set; }

        public virtual DoctorAndSpecialist DoctorAndSpecialist { get; set; }
    }           
}