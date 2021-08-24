using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.NewClasses
{
    public class PasswordRest
    {
        [Key]
        [Display(Name = "Security Question")]
        [Required(ErrorMessage = "This Field Required")]
        public string S_Question { get; set; }
        [Display(Name = "Security Answer")]
        [Required(ErrorMessage = "This Field Required")]
        public string S_Answer { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This Field Required")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "7 to 15 are only allowed")]
        public string password { get; set; }
    }
}