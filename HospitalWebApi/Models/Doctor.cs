using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class Doctor
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
        public int PatientsPerHour { get; set; }
        
       public virtual ICollection<Workclock> Workclock { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<WorkDays> WorkDays { get; set; }
        public Doctor()
        {
            this.Patients = new HashSet<Patient>();
            this.WorkDays = new HashSet<WorkDays>();
            this.Workclock = new HashSet<Workclock>();
        }
    }
}