using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DoctorSearch.Interfaces;
using DoctorSearch.Models.Doctor;
using DoctorSearch.utilities;
using FuzzyString;

namespace DoctorSearch.Controllers
{
    [RoutePrefix("api/Doctors")]
    public class DoctorsController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;
        
        public DoctorsController() : this (new DoctorSearchFactory()) { }

        public DoctorsController(IFactory factory)
        {
            if (factory ==null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _databaseHelper = new DatabaseHelper(factory);
        }


        /* GET: api/Doctors
         * query the database, return all doctors information
         * List by Id
         */

        [Route("")]
        public IEnumerable<Doctor> GetDoctors()
        {
            var doctors = _databaseHelper.GetDoctors();
            var doctorsList = doctors as Doctor[] ?? doctors.ToArray();
            foreach (var doctor in doctorsList)
            {
                var specialties = _databaseHelper.GetSpecialtiesByDoctorId(doctor.DoctorId).ToList();
                doctor.Specialties = specialties;
            }

            return doctorsList;
        }


        /*Get api/Doctors/search
         * Body pass doctor
         * Return similar doctors
         * Name        Dice Distance
         * Age			Use Age Range
         * Sex			Exact Match
         * Telephone   Not consider
         * AreaCode	Arround the area(10,25,50 miles)
         * Specialties Exact Matched
         * ReviewScore Use score range
         * priority Name -> Specialties -> Score -> Age -> Area ->Sex
         * Age, Score, Sex filter in the page
         * Sex, Area not implement
         */

        [HttpPost, Route("search")]
        public IEnumerable<Doctor> SearchSimilarDoctors(Doctor doctor)
        {
            var resultDoctors = new List<Doctor>();
            var tempDoctors = new List<Doctor>();
            
            var doctors = _databaseHelper.GetDoctors().ToList();
            
            foreach (var singledoctor in doctors)
            {
                var specialties = _databaseHelper.GetSpecialtiesByDoctorId(singledoctor.DoctorId).ToList();
                singledoctor.Specialties = specialties;
            }

            //check if there is a exactly match, if so, top 1
            if (doctors.Contains(doctor))
            {
                resultDoctors.Add(doctors.Find(x => x.Equals(doctor)));//100% match
                doctors.Remove(doctor); //remove it
            }

            //check Name matchs use Dice Distance, if pass name
            //rule if result over 0.5, means similar
            if (!string.IsNullOrEmpty(doctor.Name))
            {
                tempDoctors.AddRange(doctors.Where(singledoctor => doctor.Name.SorensenDiceDistance(singledoctor.Name) < 0.5));
            }
            resultDoctors.AddRange(tempDoctors);
            tempDoctors.Clear();

            /*Check Specialties
             * 2 possible , 
             * 1. resultDoctors is not empty, get the doctors who contain the Specialties
             * 2, resultDoctors is empty, get all doctors who contain the Specialties
             * If Specialties more than one, consider individual
             */

            if (doctor.Specialties != null)
            {
                foreach (var specialty in doctor.Specialties)
                {
                    tempDoctors.AddRange(resultDoctors.Count != 0
                        ? resultDoctors.Where(t => t.Specialties.Contains(specialty)).ToList()
                        : doctors.Where(t => t.Specialties.Contains(specialty)).ToList());
                }                
            }

            //remove duplicate doctors
            resultDoctors = tempDoctors.Count != 0 ? tempDoctors.Distinct(new DoctorComparer()).ToList() : tempDoctors;
            
            return resultDoctors;
        }
    }
}
