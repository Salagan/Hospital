using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class WorkDaysApiModel
    {
        public int Id { get; set; }
        public Days Days { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public bool IsWorking { get; set; }
    }
}