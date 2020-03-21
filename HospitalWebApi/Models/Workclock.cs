using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class Workclock
    {
        public int Id { set; get; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public int AppointHour { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}