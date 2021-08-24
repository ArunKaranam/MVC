using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Models
{
    public class HospitalAdmin
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int H_Id { get; set; }
        [Required(ErrorMessage = "Hospital name is required")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "4 to 15 are only allowed")]
        [Display(Name ="Hospital Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hospital Address is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(250)]
       // [MaxWordAttributes(50, ErrorMessage = "There are too many words in {0}.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "city name is required")]


         public string City { get; set; }
        [Required(ErrorMessage = "state name is required")]

        public string State { get; set; }
        [Required(ErrorMessage = "Zipcode is required")]

        public int ZipCode { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string Certification { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        [Display(Name = "Successful Operations")]
        public string SuccessfulOperation { get; set; }
        [StringLength(250)]
        [DataType(DataType.MultilineText)]

        public string Achievements { get; set; }
        public string Speciality { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string Facilities { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(250)]

        public string Treatment { get; set; }
        [Required(ErrorMessage ="User name is required")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "7 to 15 are only allowed")]
        [Remote("IsUserExists", "Account", ErrorMessage = "User Name already in use")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "password is required")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "7 to 15 are only allowed")]

        public string PassWord { get; set; }
        [Display(Name ="Security question")]
        [Required(ErrorMessage = "This field is required")]

        public string S_Que { get; set; }
        [Display(Name = "Security answer")]


        [Required(ErrorMessage = "This field is required")]

        public string S_ANSWER { get; set; }
        [Required(ErrorMessage = "select, userrole is required")]

        public string RoleName { get; set; }

        public bool Approved { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<DoctorUpdate> DoctorUpdates { get; set; }

        public virtual ICollection<HospitalAdminSchedule> HospitalAdminSchedules { get; set; }









    }
}