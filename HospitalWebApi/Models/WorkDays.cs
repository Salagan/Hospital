using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class WorkDays
    {
        public int Id { get; set; }
        public Days Days { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public bool IsWorking { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public WorkDays()
        {
            this.Doctors = new HashSet<Doctor>();
        }
    }

    public enum Days
    {
        Mondey, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
}