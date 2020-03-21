using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class PatientApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int Flag { get; set; }
        public virtual IEnumerable<Doctor> Doctor { get; set; }
        public virtual IEnumerable<Workclock> Workclock { get; set; }
    }
}