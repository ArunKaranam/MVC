using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.NewClasses
{
    public class GetUserName
    {
        [Key]
        [Display(Name = "Security Question")]
        [Required(ErrorMessage = "This Field Required")]
        public string S_Question { get; set; }
        [Display(Name = "Security Answer")]
        [Required(ErrorMessage = "This Field Required")]
        public string S_Answer { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public long Phone { get; set; }
    }
}