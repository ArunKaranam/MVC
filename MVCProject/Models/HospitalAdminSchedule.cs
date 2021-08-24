using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class HospitalAdminSchedule
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sch_Id { get; set; }
        [ForeignKey("Doctor")]
        [Display(Name = "Doctor Name")]
        public int D_Id { get; set; }
        [ForeignKey("HospitalAdmin")]
        [Display(Name = "Hospital Name")]
        public int H_Id { get; set; }
        [Required(ErrorMessage = "Data is required ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "time is required ")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime S_Time { get; set; }

        [Required(ErrorMessage = "time is required ")]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime E_Time { get; set; }
        [Required(ErrorMessage = "Provide the Puropse")]
        [DataType(DataType.MultilineText)]
        public String Purpose { get; set; }


        public virtual Doctor Doctor { get; set; }
        public virtual HospitalAdmin HospitalAdmin { get; set; }
    }
}