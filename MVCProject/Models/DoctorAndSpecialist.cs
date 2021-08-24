using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class DoctorAndSpecialist
    {
        [Key]
        public int S_Id { get; set; }
        public string  Specialists { get; set; }

        public virtual ICollection<DoctorUpdate> DoctorUpdates { get; set; }
    }
}