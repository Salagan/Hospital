using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalWebApi.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace HospitalWebApi.Controllers
{
    public class PatientsController : ApiController
    {
        private HospitalContext db = new HospitalContext();
        private IMapper mapper = WebApiApplication.Config;

        [Route("api/patients")]
        // GET: api/Patients
        public IHttpActionResult GetPatients()
        {
            var patient = db.Patients.ProjectTo<PatientApiModel>(mapper.ConfigurationProvider).ToList();
            return Ok(patient);
        }

        // GET: api/Patients/5
        [ResponseType(typeof(Patient))]
        [Route("api/patient/{id:int}")]
        public IHttpActionResult GetPatient(int id)
        {
            var patient = db.Patients.ProjectTo<PatientApiModel>(mapper.ConfigurationProvider)
                .FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        [Route("api/patient/{id:int}")]
        public IHttpActionResult PutPatient(int id, [FromBody]PatientApiModel pat)
        {
            var patient = db.Patients.FirstOrDefault(p => p.Id == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (patient == null)
            {
                return BadRequest();
            }

            mapper.Map(pat, patient);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(db.Patients.ProjectTo<PatientApiModel>(mapper.ConfigurationProvider).FirstOrDefault(p=>p.Id==id));
        }

        // POST: api/Patients
        [ResponseType(typeof(Patient))]
        [Route("api/patients")]
        public IHttpActionResult PostPatient([FromBody]PatientApiModel pat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var patient = mapper.Map<Patient>(pat);
            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patient))]
        [Route("api/patient/{id:int}")]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            db.SaveChanges();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.Id == id) > 0;
        }
    }
}