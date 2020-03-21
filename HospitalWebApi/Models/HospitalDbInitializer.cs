using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HospitalWebApi.Models
{
    public class HospitalDbInitializer : DropCreateDatabaseIfModelChanges<HospitalContext>
    {
        protected override void Seed(HospitalContext context)
        {
            var patients = new List<Patient>()
            {
                new Patient {Name = "Marko", BirthDate = DateTime.Parse("1996-06-08"), Gender = Gender.Male, Flag = 2},
                new Patient {Name = "Luna", BirthDate = DateTime.Parse("1995-11-25"), Gender = Gender.Female, Flag = 3},
                new Patient {Name = "Polo", BirthDate = DateTime.Parse("1991-08-13"), Gender = Gender.Male, Flag = 2,
                Workclock = new List<Workclock>{ new Workclock { DoctorId = 1, AppointHour = 9, Date = DateTime.Parse("2019-11-28") } } }
            };

            var doctors = new List<Doctor>()
            {
                new Doctor {FirstName = "Anton", LastName = "Fisher", Birthdate = DateTime.Parse("1989-07-26"), Specialization = "Head Doctor", Experience = 6},
                new Doctor {FirstName = "Andriy", LastName = "Salagan", Birthdate = DateTime.Parse("1990-07-12"), Specialization = "Surgeon", Experience = 2},
                new Doctor {FirstName = "Diana", LastName = "Budkevich", Birthdate = DateTime.Parse("1991-09-02"), Specialization = "Psychologist", Experience = 2,
                    WorkDays = new List<WorkDays>{ new WorkDays { Days = Days.Mondey, TimeEnd = new DateTime(2000, 1, 1, 18, 0, 0),
                    TimeStart = new DateTime(2000,1,1,9,0,0)} } }
            };

            patients.ForEach(x => context.Patients.Add(x));
            doctors.ForEach(x => context.Doctors.Add(x));

            context.SaveChanges();
        }
    }
}