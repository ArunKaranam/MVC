using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class FeedBack
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int F_Id { get; set; }
        public string Name { get; set; }

        public  string answer { get; set; }
        public bool Review { get; set; }
    }
}