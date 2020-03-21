﻿using HospitalWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace HospitalWebApi.Controllers
{
    public class WorkclockController : ApiController
    {
        private HospitalContext db = new HospitalContext();
        private IMapper mapper = WebApiApplication.Config;

        [Route("api/workclock")]
        public IHttpActionResult GetWorkclock()
        {
            var clock = db.Workclock.ProjectTo<WorkclockApiModel>(mapper.ConfigurationProvider).ToList();
            //var clock = db.Workclock.Select
            return Ok(clock);
        }
        [Route("api/workclock/{id:int}")]
        public IHttpActionResult GetWorkClocl(int id)
        {
            var clock = db.Workclock.ProjectTo<WorkclockApiModel>(mapper.ConfigurationProvider).FirstOrDefault(c => c.Id == id);
            if (clock != null)
            {
                return Ok(clock);
            }
            return NotFound();
        }
        [Route("api/workclock")]
        public IHttpActionResult PostWorkclock([FromBody] WorkclockApiModel wc)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var doc = db.Doctors.Where(i => i.Id == wc.DoctorId).Select(h => h.WorkDays).FirstOrDefault();
            var enm = (Days)Enum.Parse(typeof(Days), wc.Date.DayOfWeek.ToString());

            if (doc.Where(d => d.Days == enm).Any())
            {
                var clock = mapper.Map<Workclock>(wc);

                db.Workclock.Add(clock);
                try
                {
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [Route("api/workclock/{id:int}")]
        public IHttpActionResult PutWorkclock( int id, [FromBody] Workclock wc)
        {
            var clock = db.Workclock.FirstOrDefault(c => c.Id == id);// божествене знамення починай тут
            if (clock == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            mapper.Map(wc, clock);
            try
            {
                db.SaveChanges();
                return Ok(db.Workclock.ProjectTo<WorkclockApiModel>(mapper.ConfigurationProvider).FirstOrDefault(c=>c.Id == id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
        [Route("api/workclock/{id:int}")]
        public IHttpActionResult DeleteWorkclock(int id)
        {
            var clock = db.Workclock.FirstOrDefault(c => c.Id == id);
            if (clock == null)
            {
                return NotFound();
            }

            db.Workclock.Remove(clock);
            db.SaveChanges();
            return Ok(db.Workclock.ToList());
        }

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
