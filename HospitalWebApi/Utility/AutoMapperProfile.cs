using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalWebApi.Models;

namespace HospitalWebApi.Utility
{
    public class AutoMapperProfile: AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Doctor, DoctorApiModel>().ReverseMap();
            CreateMap<WorkDays, WorkDaysApiModel>().ReverseMap();
            CreateMap<Patient, PatientApiModel>().ReverseMap();
            CreateMap<Workclock, WorkclockApiModel>().ReverseMap();
        }
    }
}