using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class DoctorApiModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
        public int PatientsPerHour { get; set; }
        public  IEnumerable<WorkDaysApiModel> WorkDays { get; set; }
        public IEnumerable<PatientApiModel> Patient { get; set; }
        public DoctorApiModel()
        {
            this.WorkDays = new HashSet<WorkDaysApiModel>();
            this.Patient = new HashSet<PatientApiModel>();

        }
    }
}