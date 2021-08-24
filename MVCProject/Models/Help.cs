using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Help
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int I_Id { get; set; }
        public string Issue { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime TicketDate { get; set; }
        public string Solution { get; set; }
        [Display(Name ="User Id")]
        public int U_Id { get; set; }

        /*public int D_Id { get; set; }*/

    }
}