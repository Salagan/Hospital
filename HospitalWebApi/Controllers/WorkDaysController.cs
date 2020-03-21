using HospitalWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntityFramework.Include.Extensions;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;
namespace HospitalWebApi.Controllers
{
    public class WorkDaysController : ApiController
    {
        private HospitalContext db = new HospitalContext();

        //get workdays

        public IEnumerable<WorkDaysApiModel> GetWorkDays()
        {
            var Days = db.WorkDays.ProjectTo<WorkDaysApiModel>(WebApiApplication.Config.ConfigurationProvider).ToList();
            return Days;
        }
        // api/doctors/id/workdays
        [Route("api/doctors/{id:int}/workdays")]
        public IHttpActionResult GetDoctorWorkDays(int id)
        {
            var doctor = db.Doctors.ProjectTo<DoctorApiModel>(WebApiApplication.Config.ConfigurationProvider)
                                   .FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.WorkDays.ToList());

        } 

        //Post workDays for doctor
        [Route("api/doctors/{id:int}/workdays")]
        public IHttpActionResult PostDoctorWorkDays([FromBody]WorkDaysApiModel wD, int id)
        {
           var doctor = db.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var workDays = WebApiApplication.Config.Map<WorkDays>(wD);
            doctor.WorkDays.Add(workDays);
            
           
            db.SaveChanges();

            return Ok(db.Doctors.ProjectTo<DoctorApiModel>(WebApiApplication.Config.ConfigurationProvider).FirstOrDefault(d => d.Id == id).WorkDays.ToList()); 
        }

        //Put doctorsWorkDays
        [Route("api/doctors/{doctorId:int}/workdays/{workDayId:int}")]
        public IHttpActionResult PutDoctorsWorkDays ([FromBody] WorkDaysApiModel workDays, int doctorId, int workDayId)
        {
           WorkDays days = db.WorkDays.FirstOrDefault(w => w.Id == workDayId);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            if (days == null)
            {
                return BadRequest("bad days");
            }

            WebApiApplication.Config.Map(workDays, days);

            db.SaveChanges();
            var doctor = db.Doctors.ProjectTo<DoctorApiModel>(WebApiApplication.Config.ConfigurationProvider).FirstOrDefault(d => d.Id == doctorId);
            return Ok(doctor);
        }
        //Delete Doctors WorkDays
        [Route("api/doctors/{doctorId:int}/workdays/{workDayId:int}")]
        public IHttpActionResult DeleteDoctorsWorkDay (int doctorId, int workDayId)
        {
            Doctor doctor = db.Doctors.FirstOrDefault(d => d.Id == doctorId);
            WorkDays days = db.WorkDays.FirstOrDefault(w => w.Id == workDayId);
            if (doctor == null)
            {
                return BadRequest("bad doctor");
            }
            else if (days == null)
            {
                return BadRequest("bad days");
            }

            db.WorkDays.Remove(days);
            db.SaveChanges();
            return Ok(doctor.WorkDays);

        }
        //dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }

    
}
