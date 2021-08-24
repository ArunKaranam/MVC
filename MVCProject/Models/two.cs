using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class two
    {
        MyDbContext db = new MyDbContext();

        public List<HospitalAdmin> Hospitals { get; set; }
        
        public List<Doctor> Doctors { get; set; }
        






    }
}