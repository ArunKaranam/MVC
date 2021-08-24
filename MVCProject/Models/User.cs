using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Field is requrird")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Field is requrird")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Field is requrird")]

        public string RoleName { get; set; }

     /*  public virtual ICollection<UserRole> UserRoles { get; set; }*/
    }
}