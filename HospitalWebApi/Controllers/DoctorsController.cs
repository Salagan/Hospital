using HospitalWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalWebApi.Utility;

namespace HospitalWebApi.Controllers
{
    
    public class DoctorsController : ApiController
    {
        private HospitalContext db = new HospitalContext();
        

        //GET list of doctors
        [Route("api/doctors")]
            public IHttpActionResult GetDoctors()
        {
            var doctor = db.Doctors.ProjectTo<DoctorApiModel>(WebApiApplication.Config.ConfigurationProvider).ToList();
            return Ok(doctor);
        }


        //GET doctor

        [Route("api/doctor/{id:int}")]
        public  IHttpActionResult GetDoctor(int id)
        {
           var doctor = db.Doctors.ProjectTo<DoctorApiModel>(WebApiApplication.Config.ConfigurationProvider).FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }
        //POST doctor
        [HttpPost]
        [Route("api/doctors")]
        public IHttpActionResult PostDoctor([FromBody]DoctorApiModel doc)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = WebApiApplication.Config.Map<Doctor>(doc);
            db.Doctors.Add(doctor);
            db.SaveChanges();

            return Ok(doctor);
        }
        //PUT dcotor
        
        [HttpPut]
        [Route("api/doctor/{id:int}")]
        public IHttpActionResult PutDoctor(int id, [FromBody]DoctorApiModel doc)
        {
            var doctor = db.Doctors.FirstOrDefault(d => d.Id == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (doctor == null)
            {
                return BadRequest();
            }

            WebApiApplication.Config.Map(doc, doctor);

            try
            {
                db.SaveChanges();
                return Ok(db.Doctors.ProjectTo<DoctorApiModel>(WebApiApplication.Config.ConfigurationProvider).FirstOrDefault(d => d.Id == id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DELETE
        
        [HttpDelete]
        public IHttpActionResult DeleteDoctor(int id)
        {
            var query = db.Doctors.Where(d => d.Id == id).OrderBy(o => o.FirstName);
            Doctor doctor = query
                .FirstOrDefault();
            if (doctor == null)
            {
                return NotFound();
            }
            db.Doctors.Remove(doctor);
            db.SaveChanges();

            return Ok(doctor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public IEnumerable<DoctorApiModel> GetDoctors()
        //{

        //    return db.Doctors.Select(doctor => new DoctorApiModel
        //   {
        //       Birthdate = doctor.Birthdate,
        //       Experience = doctor.Experience,
        //       FirstName = doctor.FirstName,
        //       Id = doctor.Id,
        //       LastName = doctor.LastName,
        //       Specialization = doctor.Specialization,
        //       WorkDays = doctor.WorkDays.Select(workDay => new WorkDaysApiModel
        //       {
        //           Days = workDay.Days,
        //           Id = workDay.Id,
        //           IsWorking = workDay.IsWorking,
        //           TimeStart = workDay.TimeStart,
        //           TimeEnd = workDay.TimeEnd
        //       }).ToList()
        //   }).ToList();
        //}
    }
}
