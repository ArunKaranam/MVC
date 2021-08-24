using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class HospitalDepartment
    {
        [Key]
        public int Dep_Id { get; set; }
        public string Department_Name { get; set; }
        public virtual ICollection<TreatmentReport> TreatmentReports { get; set; }
    }
}